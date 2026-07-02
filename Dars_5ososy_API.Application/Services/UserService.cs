using AutoMapper;
using Dars_5ososy_API.Application.DTOs.UserDTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure.Repositories;

namespace Dars_5ososy_API.Application.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetAllStudentsAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var filteredUsers = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await GetUserRole(user.UserName);
                if (roles.Contains("Student") && !roles.Contains("Admin"))
                    filteredUsers.Add(_mapper.Map<UserDTO>(user));
            }

            return filteredUsers;
        }
        
        public async Task<List<UserDTO>> GetAllTeachersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var filteredUsers = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await GetUserRole(user.UserName);
                if (roles.Contains("Teacher") && !roles.Contains("Admin"))
                    filteredUsers.Add(_mapper.Map<UserDTO>(user));
            }

            return filteredUsers;
        }

        public async Task<UserDTO?> GetByUserNameAsync(string userName)
        {
            var user = await _userRepository.GetByUserNameAsync(userName);
            if (user == null) 
                return null;
            return _mapper.Map<UserDTO>(user);
        }
        
        public async Task<UserDTO?> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return null;
            return _mapper.Map<UserDTO>(user);
        }
        
        public async Task<UserDTO?> GetByPhoneNumberAsync(string phoneNumber)
        {
            var user = await _userRepository.GetByPhoneNumberAsync(phoneNumber);
            if (user == null) 
                return null;
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<string>?> GetUserRole(string username)
        {
            var user = await _userRepository.GetByUserNameAsync(username);
            if (user == null) 
                return null;
            return await _userRepository.GetRolesAsync(user);
        }

        public async Task<UserDTO?> CreateAsync(CreatedUserDTO createdUserDTO, string password)
        {
            var user = _mapper.Map<User>(createdUserDTO);
            var createdUser = await _userRepository.CreateAsync(user, password);
            if (createdUser == null) 
                return null;
            return _mapper.Map<UserDTO>(createdUser);
        }

        public async Task<UserDTO?> UpdateAsync(UpdatedUserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var updatedUser = await _userRepository.UpdateAsync(user);
            if (updatedUser == null) 
                return null;
            return _mapper.Map<UserDTO>(updatedUser);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}
