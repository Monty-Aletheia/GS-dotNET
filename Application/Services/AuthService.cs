using Application.Dtos.Auth;
using Application.Dtos.User;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Infra.Repositories;

namespace Application.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthService(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AuthResponseDTO> RegisterAsync(UserRequestDTO registerDTO)
        {
            User user = _mapper.Map<User>(registerDTO);

            if (await _userRepository.FindByEmailAsync(user.Email) != null)
            {
                throw new BadRequestException("This email is already in use");
            }

            if (!string.IsNullOrEmpty(registerDTO.FirebaseId) &&
                await _userRepository.FindByFirebaseIdAsync(registerDTO.FirebaseId) != null)
            {
                throw new BadRequestException("FirebaseId is already in use");
            }

            await _userRepository.AddAsync(user);
            var userResponse = _mapper.Map<UserResponseDTO>(user);
            var message = "User registered successfully";

            return new AuthResponseDTO(message, userResponse);
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginRequestDTO loginDTO)
        {
            var user = await _userRepository.FindByEmailAsync(loginDTO.Email);

            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            if (!user.Password.Equals(loginDTO.Password))
            {
                throw new UnauthorizedException("Invalid credentials");
            }

            var message = "Login successful";
            var userResponse = _mapper.Map<UserResponseDTO>(user);

            return new AuthResponseDTO(message, userResponse);
        }
    }
}
