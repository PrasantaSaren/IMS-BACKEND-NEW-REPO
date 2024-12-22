using Inventory.AppCode;
using Inventory.Infrastructure.ServicesInstaller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;
using Inventory.Infrastructure.Options;
using Common_Helper.CommonHelper;
using Inventory.Repository.DBContext;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Inventory.Extensions;
using Microsoft.EntityFrameworkCore;
using Inventory.Repository.IService;
using Inventory.Repository.Service;

namespace Inventory.Infrastructure
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private readonly ISwaggerProviderOptions? swaggerOptions;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AuditLog.Configuration = Configuration;
            this.swaggerOptions = this.Configuration.GetSection("SwaggerOptions").Get<SwaggerProviderOptions>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // CORS Policy
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            // Installers for other services
            var installers = typeof(Startup).Assembly.ExportedTypes
                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
            installers.ForEach(installer => installer.InstallerServices(services, Configuration));

            // JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:secretKey"]!))
                };
            });

            // Add MVC services
            services.AddMvc(options => options.EnableEndpointRouting = false);

            // Swagger setup
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = swaggerOptions?.Title, Version = swaggerOptions?.Version });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                                    Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                          },
                          Array.Empty<string>()
                    }
                });
            });

            // DbContext configuration (single call)
            services.AddDbContext<IMSV2Context>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("ProjectConnection"));
                opt.EnableSensitiveDataLogging();  // Enable sensitive logging if required
            });
            // Added services for DI
            services.AddScoped<IRequisitionRepository, RequisitionRepository>();
            // SmtpSettings configuration
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));

            // Add controllers
            services.AddControllers();
            services.AddEndpointsApiExplorer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            UseExceptionHandler(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            // Set up service locator for ConnectionHelper Class
            AppServicesHelper.Services = app.ApplicationServices;

            // Swagger setup
            SetupSwagger(app);

            // Enable CORS
            app.UseCors("CorsPolicy");

            // Enable static files
            app.UseStaticFiles();

            // Enable Swagger UI
            app.UseSwagger();

            // Enable Authentication and Authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Enable HTTPS redirection
            app.UseHttpsRedirection();

            // Use MVC routing
            app.UseMvc();
        }

        private static void UseExceptionHandler(IApplicationBuilder app)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = Text.Plain;
                    await context.Response.WriteAsync("An exception was thrown.");
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                    {
                        await context.Response.WriteAsync("The file was not found.");
                    }
                    if (exceptionHandlerPathFeature?.Path == "/")
                    {
                        await context.Response.WriteAsync("Page: Home.");
                    }
                });
            });
        }

        private void SetupSwagger(IApplicationBuilder app)
        {
            app.UseSwagger(option =>
            {
                option.RouteTemplate = swaggerOptions?.JsonRoute;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerOptions?.UIEndpoint, swaggerOptions?.Description);
            });
        }
    }
}
