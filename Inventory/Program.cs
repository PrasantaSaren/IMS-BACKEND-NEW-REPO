using Inventory.Extensions;
using Inventory.Infrastructure;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace Inventory;
public static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                //var buildConfig = config.Build();
                //var keyVaultEndpoint = "";
                //keyVaultEndpoint = GetKeyVaultEndpoint(buildConfig);

                //if (!string.IsNullOrEmpty(keyVaultEndpoint))
                //{
                //    var azureServiceTokenProvider = new AzureServiceTokenProvider();

                //    // Setup the key vault client
                //    var keyVaultClient = new KeyVaultClient(
                //        new KeyVaultClient.AuthenticationCallback(
                //            azureServiceTokenProvider.KeyVaultTokenCallback));

                //    // Add keyvault to config
                //    config.AddAzureKeyVault(
                //        keyVaultEndpoint,
                //        keyVaultClient,
                //        new DefaultKeyVaultSecretManager());
                //}
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

    //private static string GetKeyVaultEndpoint(IConfigurationRoot buildConfig) =>
    //    $"https://{buildConfig["AzureVault:KeyVaultName"]}.vault.azure.net";
}