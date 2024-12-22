using Inventory.Extensions;
using Inventory.Repository.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Inventory.AppCode
{
    public class ConnectionHelper
    {
        private readonly IConfiguration? Configuration;
        public ConnectionHelper(IConfiguration configuration)
        {
            Configuration= configuration;
        }
        public IDbConnection GetDbConnection()
        {
            IDbConnection connection = new SqlConnection
            {
                ConnectionString = GetMasterConnectionString()
            };
            return connection;
        }
        [return: MaybeNull]
        private string GetMasterConnectionString()
        {
            string? masterconn = string.Empty;
            try
            {
                //var cs = Configuration?[Configuration.GetConnectionString("ProjectConnection")!];
                masterconn = Configuration?["ConnectionStrings:ProjectConnection"];
            }
            catch (SqlException sqlEx)
            {
                throw new ArgumentNullException(sqlEx.Message);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException($"Configuration Error: {ex.Message}");
            }
            return masterconn;
        }
        public IMSV2Context GetDbContext()
        {
            var bizBOptionsBuilder = new DbContextOptionsBuilder<IMSV2Context>();
            bizBOptionsBuilder.UseSqlServer(GetMasterConnectionString());
            return new IMSV2Context(bizBOptionsBuilder.Options);
        }

       
    }
    public class AppServicesHelper
    {

        /// <summary>
        /// IServiceProvider
        /// </summary>
        public static IServiceProvider? Services
        {
            get; set;
        }
        /// <summary>
        /// HttpContext_Current
        /// </summary>
        public HttpContext? HttpContext_Current
        {
            get
            {
                IHttpContextAccessor? httpContextAccessor = AppServicesHelper.Services?.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
                return httpContextAccessor?.HttpContext;
            }
        }
    }
}
