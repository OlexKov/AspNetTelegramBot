using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic.Exstentions
{
    public static class BusinessLogicServiceExtentions
    {
        public static void AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IUserService, UserService>();
        }
    }
}
