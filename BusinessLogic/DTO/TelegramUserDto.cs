
namespace BusinessLogic.DTO
{
    public class TelegramUserDto
    {
        public long Id { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Vcard { get; set; } = string.Empty;
    }
}
