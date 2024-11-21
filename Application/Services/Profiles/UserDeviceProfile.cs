using Application.Dtos.UserDevice;
using AutoMapper;
using Domain.Entities;

namespace Application.Services.Profiles
{
    public class UserDeviceProfile : Profile
    {
        public UserDeviceProfile()
        {
            CreateMap<UserDeviceDTO, UserDevice>()
                .ForMember(dest => dest.EstimatedUsageHours, opt => opt.MapFrom(src => src.EstimatedUsageHours))
                .ForMember(dest => dest.Consumption, opt => opt.Ignore())
                .ForMember(dest => dest.Device, opt => opt.Ignore()) 
                .ForMember(dest => dest.User, opt => opt.Ignore());  

            CreateMap<UserDevice, UserDeviceResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EstimatedUsageHours, opt => opt.MapFrom(src => src.EstimatedUsageHours))
                .ForMember(dest => dest.Consumption, opt => opt.MapFrom(src => src.Consumption))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Device.Model))
                .ForMember(dest => dest.PowerRating, opt => opt.MapFrom(src => src.Device.PowerRating))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Device.Category))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Device.Name));
        }
    }
}
