using Application.Dtos.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequestDTO, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<User, UserResponseDTO>();
        }
    }
}
