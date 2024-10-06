using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Entities;


namespace BusinessLogic.Mappers
{
    internal class TelegramUserProfile : Profile
    {
        public TelegramUserProfile()
        {
            CreateMap<TelegramUser, TelegramUserDto>().ReverseMap();
        }
    }
}
