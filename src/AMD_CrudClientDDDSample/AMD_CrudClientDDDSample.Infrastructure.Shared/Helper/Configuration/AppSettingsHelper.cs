using Microsoft.Extensions.Configuration;

namespace AMD_CrudClientDDDSample.Infrastructure.Shared.Helper.Configuration
{
    public static class AppSettingsHelper
    {
        public static string GetConfigurationAppSettings(string chave, string valor)
        {
            return GetAppSettings().GetSection(chave).GetSection(valor).Value;
        }

        private static IConfiguration GetAppSettings()
        {
            var enviromentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var result = new ConfigurationBuilder()
                 .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json")
                 .AddJsonFile($"appsettings.{enviromentVariable}.json", optional: true)
                 .Build();

            return result;
        }
    }
}