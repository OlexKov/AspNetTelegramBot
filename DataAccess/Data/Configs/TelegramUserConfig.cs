using BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Data.Configs
{
    public class TelegramUserConfig:IEntityTypeConfiguration<TelegramUser>
    {
        public void Configure(EntityTypeBuilder<TelegramUser> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
