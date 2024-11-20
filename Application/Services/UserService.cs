using Application.Dtos.User;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Infra.Repositories;

namespace Application.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(UserRepository userRepository, IMapper userMapper)
        {
            _userRepository = userRepository;
            _mapper = userMapper;
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
                
            

            return _mapper.Map<List<UserResponseDTO>>(users);
        }

        public async Task<UserResponseDTO> GetUserByIdAsync(Guid id)
        {
            var user = await FindUserByIdAsync(id);
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO> GetUserByFirebaseIdAsync(string firebaseId)
        {
            var user = await _userRepository.FindByFirebaseIdAsync(firebaseId)
                ?? throw new NotFoundException("User not found");
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO> UpdateUserAsync(Guid id, UserRequestDTO dto)
        {
            var user = await FindUserByIdAsync(id);
            _mapper.Map(dto, user);
            await _userRepository.UpdateAsync(user);
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await FindUserByIdAsync(id);
            await _userRepository.DeleteAsync(user);
        }

        // ============================
        // =          HELPERS         =
        // ============================

        private async Task<User> FindUserByIdAsync(Guid userId)
        {
            return await _userRepository.FindByIdAsync(userId)
                ?? throw new NotFoundException("User not found");
        }
    }
}