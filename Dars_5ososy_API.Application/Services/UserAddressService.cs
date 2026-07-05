using AutoMapper;
using Dars_5ososy_API.Application.DTOs.UserDTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure.Repositories;
using System.Text.RegularExpressions;

namespace Dars_5ososy_API.Application.Services
{
    public class UserAddressService
    {
        private readonly IMapper _mapper;
        private readonly IUserAddressRepository _userAddressRepository;
        private readonly IAreaRepository _areaRepository;

        public UserAddressService(IUserAddressRepository userAddressRepository, IAreaRepository areaRepository, IMapper mapper)
        {
            _userAddressRepository = userAddressRepository;
            _areaRepository = areaRepository;
            _mapper = mapper;
        }

        public async Task<UserAddressDTO?> CreateUserAddressAsync(UserAddressDTO entity)
        {
            var existingUserAddress = await _userAddressRepository.GetByUsernameAsync(entity.UserName);
            if (existingUserAddress != null)
                return null;

            var existingArea = Regex.IsMatch(entity.Area, @"^[A-Za-z\s'-]+$")
                ? await _areaRepository.GetByEnglishNameAsync(entity.Area)
                : await _areaRepository.GetByArabicNameAsync(entity.Area);

            if (existingArea == null)
                return null;

            existingUserAddress.AreaId = existingArea.Id;
            existingUserAddress.Area = existingArea;

            var createdUserAddress = await _userAddressRepository.CreateAsync(_mapper.Map<UserAddress>(entity));
            if (createdUserAddress == null) 
                return null;
            return _mapper.Map<UserAddressDTO>(createdUserAddress);
        }

        public async Task<bool> DeleteUserAddressAsync(string username)
        {
            var existingUserAddress = await _userAddressRepository.GetByUsernameAsync(username);
            if (existingUserAddress == null)
                return false;
            var deleted = await _userAddressRepository.DeleteAsync(existingUserAddress);
            return deleted;
        }

        public async Task<List<UserAddressDTO>> GetAllUserAddressesAsync()
        {
            var userAddresses = await _userAddressRepository.GetAllAsync();
            return _mapper.Map<List<UserAddressDTO>>(userAddresses);
        }

        public async Task<UserAddressDTO?> GetUserAddressByUsernameAsync(string username)
        {
            var userAddress = await _userAddressRepository.GetByUsernameAsync(username);
            if (userAddress == null)
                return null;
            return _mapper.Map<UserAddressDTO>(userAddress);
        }

        public async Task<UserAddressDTO?> UpdateUserAddressAsync(UserAddressDTO entity)
        {
            var existingUserAddress = await _userAddressRepository.GetByUsernameAsync(entity.UserName);
            if (existingUserAddress == null)
                return null;
            
            var updatedUserAddress = _mapper.Map<UserAddress>(entity);
            var existingArea = Regex.IsMatch(entity.Area, @"^[A-Za-z\s'-]+$")
                ? await _areaRepository.GetByEnglishNameAsync(entity.Area)
                : await _areaRepository.GetByArabicNameAsync(entity.Area); updatedUserAddress.Area = existingArea;

            if (existingArea == null)
                return null;

            updatedUserAddress.AreaId = existingArea?.Id ?? 0;
            updatedUserAddress.Area = existingArea;
            
            var result = await _userAddressRepository.UpdateAsync(updatedUserAddress);
            if (result == null)
                return null;
            return _mapper.Map<UserAddressDTO>(result);
        }
    }
}
