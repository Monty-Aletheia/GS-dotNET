using Application.Dtos.Device;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Infra.Repositories;

namespace Application.Services
{
    public class DeviceService
    {
        private readonly DeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public DeviceService(DeviceRepository deviceRepository, IMapper deviceMapper)
        {
            _deviceRepository = deviceRepository;
            _mapper = deviceMapper;
        }

        public async Task<List<DeviceResponseDTO>> GetDevicesAsync()
        {
            var devices = await _deviceRepository.GetAllAsync();
            return _mapper.Map<List<DeviceResponseDTO>>(devices) ;
        }
        public async Task<DeviceResponseDTO> GetDeviceByIdAsync(Guid deviceId)
        {
            var device = await FindDeviceByIdAsync(deviceId);
            return _mapper.Map<DeviceResponseDTO>(device);
        }

        public async Task<DeviceResponseDTO> CreateDeviceAsync(DeviceRequestDTO deviceRequestDTO)
        {
            var device = _mapper.Map<Device>(deviceRequestDTO);
            await _deviceRepository.AddAsync(device);
            return _mapper.Map<DeviceResponseDTO>(device);
        }

        public async Task<DeviceResponseDTO> UpdateDeviceAsync(Guid deviceId, DeviceRequestDTO deviceRequestDTO)
        {
            var device = await FindDeviceByIdAsync(deviceId);
            _mapper.Map(deviceRequestDTO, device);
            await _deviceRepository.UpdateAsync(device);

            return _mapper.Map<DeviceResponseDTO>(device);
        }

        public async Task DeleteDeviceAsync(Guid deviceId)
        {
            var device = await FindDeviceByIdAsync(deviceId);
            await _deviceRepository.DeleteAsync(device);
        }

        // ============================
        // =          HELPERS         =
        // ============================

        private async Task<Device> FindDeviceByIdAsync(Guid id)
        {
            var device = await _deviceRepository.FindByIdAsync(id);
            if (device == null)
            {
                throw new NotFoundException("Device not found");
            }
            return device;
        }
    }
}
