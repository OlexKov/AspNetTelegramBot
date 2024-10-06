using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Entities;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using BusinessLogic.Specifications;
using System.Net;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<TelegramUser> userRepo;
        private readonly IMapper mapper;

        public UserService(IRepository<TelegramUser> userRepo, IMapper mapper)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
        }
        public async Task<long> CreateUserAsync(TelegramUserDto user, bool admin = false)
        {
            TelegramUser newUser = mapper.Map<TelegramUser>(user);
            newUser.IsAdmin = admin;
            await userRepo.InsertAsync(newUser);
            await userRepo.SaveAsync();
            return  user.Id;
        }

        public async Task<IEnumerable<TelegramUserDto>> GetAllAdminsAsync()
        {
            var admins = await userRepo.GetListBySpec(new TelegramUserSpecs.GetAllAdmins());
            return mapper.Map<IEnumerable<TelegramUserDto>>(admins);
        }

        public async Task<IEnumerable<TelegramUserDto>> GetAllUsersAsync()
        {
            var users = await userRepo.GetListBySpec(new TelegramUserSpecs.GetAllUsers());
            return mapper.Map<IEnumerable<TelegramUserDto>>(users);
        }

        public async Task<TelegramUserDto> GetByIdAsync(long id)
        {
            var user = await userRepo.GetItemBySpec(new TelegramUserSpecs.GetById(id)) 
                ?? throw  new HttpException("Invalid user ID", HttpStatusCode.BadRequest);
            return mapper.Map<TelegramUserDto>(user);
        }
    }
}
