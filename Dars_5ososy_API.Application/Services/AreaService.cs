using AutoMapper;
using Dars_5ososy_API.Application.DTOs.AddressDTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure.Repositories;

namespace Dars_5ososy_API.Application.Services
{
    public class AreaService
    {
        private readonly IMapper _mapper;
        private readonly AreaRepository _AreaRepository;

        public AreaService(AreaRepository AreaRepository, IMapper mapper)
        {
            _AreaRepository = AreaRepository;
            _mapper = mapper;
        }

        public async Task<List<AreaDTO>> GetAllAsync()
        {
            var Areas = await _AreaRepository.GetAllAsync();
            return _mapper.Map<List<AreaDTO>>(Areas);
        }

        public async Task<AreaDTO?> GetByArabicNameAsync(string AreaArabicName)
        {
            var Area = await _AreaRepository.GetByArabicNameAsync(AreaArabicName);
            if (Area == null) return null;
            return _mapper.Map<AreaDTO>(Area);
        }
        
        public async Task<AreaDTO?> GetByEnglishNameAsync(string AreaEnglishName)
        {
            var Area = await _AreaRepository.GetByEnglishNameAsync(AreaEnglishName);
            if (Area == null) return null;
            return _mapper.Map<AreaDTO>(Area);
        }

        public async Task<AreaDTO?> CreateAsync(AreaDTO createdAreaDTO)
        {
            var Area = _mapper.Map<Area>(createdAreaDTO);
            var createdArea = await _AreaRepository.CreateAsync(Area);
            if (createdArea == null) return null;
            return _mapper.Map<AreaDTO>(createdArea);
        }

        public async Task<AreaDTO?> UpdateAsync(AreaDTO AreaDto)
        {
            var Area = _mapper.Map<Area>(AreaDto);
            var updatedArea = await _AreaRepository.UpdateAsync(Area);
            if (updatedArea == null) return null;
            return _mapper.Map<AreaDTO>(updatedArea);
        }

        public async Task<bool> DeleteByArabicNameAsync(string AreaArabicName)
        {
            return await _AreaRepository.DeleteByArabicNameAsync(AreaArabicName);
        }

        public async Task<bool> DeleteByEnglishNameAsync(string AreaEnglishName)
        {
            return await _AreaRepository.DeleteByEnglishNameAsync(AreaEnglishName);
        }

        public async Task<List<AreaDTO>> GetAllByGovernorateEnglishNameAsync(string governorateEnglishName)
        {
            var Areas = await _AreaRepository.GetAllByGovernorateEnglishNameAsync(governorateEnglishName);
            return _mapper.Map<List<AreaDTO>>(Areas);
        }

        public async Task<List<AreaDTO>> GetAllByGovernorateArabicNameAsync(string governorateArabicName)
        {
            var Areas = await _AreaRepository.GetAllByGovernorateArabicNameAsync(governorateArabicName);
            return _mapper.Map<List<AreaDTO>>(Areas);
        }

        public async Task<List<AreaDTO>> GetAllByProvinceEnglishNameAsync(string provinceEnglishName)
        {
            var Areas = await _AreaRepository.GetAllByProvinceEnglishNameAsync(provinceEnglishName);
            return _mapper.Map<List<AreaDTO>>(Areas);
        }

        public async Task<List<AreaDTO>> GetAllByProvinceArabicNameAsync(string provinceArabicName)
        {
            var Areas = await _AreaRepository.GetAllByProvinceArabicNameAsync(provinceArabicName);
            return _mapper.Map<List<AreaDTO>>(Areas);
        }
    }
}
