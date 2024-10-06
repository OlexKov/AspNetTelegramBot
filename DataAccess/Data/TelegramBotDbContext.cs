using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess.Data
{
    public class TelegramBotDbContext:DbContext
    {
        public TelegramBotDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
