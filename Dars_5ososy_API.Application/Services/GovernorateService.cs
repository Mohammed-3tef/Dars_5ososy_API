using AutoMapper;
using Dars_5ososy_API.Application.DTOs.AddressDTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure.Repositories;

namespace Dars_5ososy_API.Application.Services
{
    public class GovernorateService
    {
        private readonly IMapper _mapper;
        private readonly GovernorateRepository _GovernorateRepository;

        public GovernorateService(GovernorateRepository GovernorateRepository, IMapper mapper)
        {
            _GovernorateRepository = GovernorateRepository;
            _mapper = mapper;
        }

        public async Task<List<GovernorateDTO>> GetAllAsync()
        {
            var Governorates = await _GovernorateRepository.GetAllAsync();
            return _mapper.Map<List<GovernorateDTO>>(Governorates);
        }

        public async Task<GovernorateDTO?> GetByArabicNameAsync(string GovernorateArabicName)
        {
            var Governorate = await _GovernorateRepository.GetByArabicNameAsync(GovernorateArabicName);
            if (Governorate == null) return null;
            return _mapper.Map<GovernorateDTO>(Governorate);
        }
        
        public async Task<GovernorateDTO?> GetByEnglishNameAsync(string GovernorateEnglishName)
        {
            var Governorate = await _GovernorateRepository.GetByEnglishNameAsync(GovernorateEnglishName);
            if (Governorate == null) return null;
            return _mapper.Map<GovernorateDTO>(Governorate);
        }

        public async Task<GovernorateDTO?> CreateAsync(GovernorateDTO createdGovernorateDTO)
        {
            var Governorate = _mapper.Map<Governorate>(createdGovernorateDTO);
            var createdGovernorate = await _GovernorateRepository.CreateAsync(Governorate);
            if (createdGovernorate == null) return null;
            return _mapper.Map<GovernorateDTO>(createdGovernorate);
        }

        public async Task<GovernorateDTO?> UpdateAsync(GovernorateDTO GovernorateDto)
        {
            var Governorate = _mapper.Map<Governorate>(GovernorateDto);
            var updatedGovernorate = await _GovernorateRepository.UpdateAsync(Governorate);
            if (updatedGovernorate == null) return null;
            return _mapper.Map<GovernorateDTO>(updatedGovernorate);
        }

        public async Task<bool> DeleteByArabicNameAsync(string GovernorateArabicName)
        {
            return await _GovernorateRepository.DeleteByArabicNameAsync(GovernorateArabicName);
        }

        public async Task<bool> DeleteByEnglishNameAsync(string GovernorateEnglishName)
        {
            return await _GovernorateRepository.DeleteByEnglishNameAsync(GovernorateEnglishName);
        }

        public async Task<List<GovernorateDTO>> GetAllByProvinceEnglishNameAsync(string provinceEnglishName)
        {
            var Governorates = await _GovernorateRepository.GetAllByProvinceEnglishNameAsync(provinceEnglishName);
            return _mapper.Map<List<GovernorateDTO>>(Governorates);
        }

        public async Task<List<GovernorateDTO>> GetAllByProvinceArabicNameAsync(string provinceArabicName)
        {
            var Governorates = await _GovernorateRepository.GetAllByProvinceArabicNameAsync(provinceArabicName);
            return _mapper.Map<List<GovernorateDTO>>(Governorates);
        }
    }
}
