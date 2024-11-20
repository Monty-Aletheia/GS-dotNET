using Application.Dtos.Device;
using Domain.Entities;
using AutoMapper;

namespace Application.Services.Profiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<DeviceRequestDTO, Device>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<Device, DeviceResponseDTO>();

        }
    }
}
