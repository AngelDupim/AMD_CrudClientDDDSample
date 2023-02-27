using AMD_CrudClientDDDSample.Infrastructure.Shared.Helper.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AMD_CrudClientDDDSample.Infrastructure.Shared.JWT
{
    public static class JwtConfiguration
    {
        public static IServiceCollection addJWT(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(AppSettingsHelper.GetConfigurationAppSettings("SettingsJWT", "Secret"));

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}