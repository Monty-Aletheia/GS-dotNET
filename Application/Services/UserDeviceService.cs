using Application.Dtos.UserDevice;
using Application.Exceptions;
using Application.Services.Profiles;
using AutoMapper;
using Domain.Entities;
using Infra.Repositories;

namespace Application.Services;

public class UserDeviceService
{
    private readonly DeviceRepository _deviceRepository;
    private readonly UserDeviceRepository _userDeviceRepository;
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserDeviceService(
        DeviceRepository deviceRepository,
        UserDeviceRepository userDeviceRepository,
        UserRepository userRepository,
        IMapper mapper)
    {
        _deviceRepository = deviceRepository;
        _userDeviceRepository = userDeviceRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<UserDeviceResponseDTO>> GetAllUserDevicesAsync(Guid userId)
    {
        var userDevices = await _userDeviceRepository.FindByUserIdAsync(userId);
        if (userDevices == null || !userDevices.Any())
        {
            throw new NotFoundException("No devices found for the user.");
        }

        return _mapper.Map<List<UserDeviceResponseDTO>>(userDevices);
    }

    public async Task<UserDeviceResponseDTO> GetUserDeviceAsync(Guid userId, Guid id)
    {
        var userDevice = await _userDeviceRepository.FindByIdAndUserIdAsync(id, userId) 
            ?? throw new NotFoundException("No devices found for the user.");

        return _mapper.Map<UserDeviceResponseDTO>(userDevice);
    }


    public async Task<UserDeviceResponseDTO> AddDeviceToUserAsync(Guid userId, UserDeviceDTO dto)
    {
        var user = await _userRepository.FindByIdAsync(userId) ?? throw new NotFoundException("User not found.");
        var device = await _deviceRepository.FindByIdAsync(dto.DeviceId) ?? throw new NotFoundException("Device not found.");

        var userDevice = _mapper.Map<UserDevice>(dto);

        userDevice.Consumption = (userDevice.EstimatedUsageHours == 0 || device.PowerRating == 0)
            ? 0 
            : (device.PowerRating / 1000) * userDevice.EstimatedUsageHours;

        userDevice.User = user;
        userDevice.Device = device;

        await _userDeviceRepository.AddAsync(userDevice);

        return _mapper.Map<UserDeviceResponseDTO>(userDevice);
    }

    public async Task<UserDeviceResponseDTO> UpdateUserDeviceAsync(Guid userDeviceId, Guid userId, double estimatedUsageHours)
    {
        var userDevice = await _userDeviceRepository.FindByIdAndUserIdAsync(userDeviceId, userId) 
            ?? throw new BadRequestException("Device is not associated with the user.");

        userDevice.EstimatedUsageHours = estimatedUsageHours;

        await _userDeviceRepository.UpdateAsync(userDevice);

        return _mapper.Map<UserDeviceResponseDTO>(userDevice);
    }

    public async Task RemoveDeviceFromUserAsync(Guid userId, Guid userDeviceId)
    {
        var userDevice = await _userDeviceRepository.FindByIdAndUserIdAsync(userDeviceId, userId) 
            ?? throw new NotFoundException("Device not found or not associated with the user.");
        await _userDeviceRepository.DeleteAsync(userDevice);
    }
}
