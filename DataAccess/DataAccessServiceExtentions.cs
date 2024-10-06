using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class DataAccessServiceExtentions
    {
        public static void AddTelegramDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TelegramBotDbContext>(opts =>
                opts.UseNpgsql(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
