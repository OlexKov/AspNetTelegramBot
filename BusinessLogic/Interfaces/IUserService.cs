

using BusinessLogic.DTO;

namespace BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<TelegramUserDto>> GetAllUsersAsync();
        Task<IEnumerable<TelegramUserDto>> GetAllAdminsAsync();
        Task<TelegramUserDto> GetByIdAsync(long id);
        Task<long> CreateUserAsync(TelegramUserDto user,bool admin = false);
       
    }
}
