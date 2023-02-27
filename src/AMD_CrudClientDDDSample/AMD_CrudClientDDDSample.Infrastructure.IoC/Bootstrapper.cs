using AMD_CrudClientDDDSample.Application.Handler;
using AMD_CrudClientDDDSample.Application.Handler.Interfaces;
using AMD_CrudClientDDDSample.Application.Mapper;
using AMD_CrudClientDDDSample.Domain.Handlers;
using AMD_CrudClientDDDSample.Domain.Handlers.Interfaces;
using AMD_CrudClientDDDSample.Domain.Repository.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Data.Context;
using AMD_CrudClientDDDSample.Infrastructure.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AMD_CrudClientDDDSample.Infrastructure.IoC
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddRegisterServices(this IServiceCollection services)
        {
            RegisterRepository(services);
            RegisterDomain(services);
            RegisterService(services);
            RegisterApplication(services);
            return services;
        }

        private static void RegisterService(IServiceCollection services)
        {
            services.AddSingleton(MapperConfig.RegisterMapper());            
        }

        private static void RegisterApplication(IServiceCollection services)
        {
            services.AddTransient<IClientHandlerApplication, ClientHandlerApplication>();
            services.AddTransient<IUserHendlerApplication, UserHandlerApplication>();
            services.AddTransient<IAuthHandlerApplication, AuthHandlerApplication>();
        }

        private static void RegisterDomain(IServiceCollection services)
        {
            services.AddTransient<IClientHandler, ClientHandler>();
            services.AddTransient<IUserHandler, UserHandler>();
            services.AddTransient<IAuthHandler, AuthHandler>();
        }

        private static void RegisterRepository(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddSingleton<IContext, Context>();
        }
    }
}