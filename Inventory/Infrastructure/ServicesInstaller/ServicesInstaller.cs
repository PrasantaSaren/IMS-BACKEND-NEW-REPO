using Inventory.AppCode;
using Inventory.AppCode.Helper;
using Inventory.Repository.IService;
using Inventory.Repository.Service;
using InventoryAPI.Repository;

namespace Inventory.Infrastructure.ServicesInstaller
{
    public interface IInstaller
    {
        void InstallerServices(IServiceCollection services, IConfiguration configuration);
    }
    public class ServicesInstaller: IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMailHelper, MailHelper>();
            services.AddScoped<ISessionHelper, SessionHelper>();
            services.AddScoped<IMasterRepository, MasterRepository>();
            services.AddScoped<IGlobalService, GlobalService>();

        }
    }
}
