using AutoMapper;
using Dars_5ososy_API.Application.DTOs.AddressDTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure.Repositories;

namespace Dars_5ososy_API.Application.Services
{
    public class ProvinceService
    {
        private readonly IMapper _mapper;
        private readonly ProvinceRepository _ProvinceRepository;

        public ProvinceService(ProvinceRepository ProvinceRepository, IMapper mapper)
        {
            _ProvinceRepository = ProvinceRepository;
            _mapper = mapper;
        }

        public async Task<List<ProvinceDTO>> GetAllAsync()
        {
            var Provinces = await _ProvinceRepository.GetAllAsync();
            return _mapper.Map<List<ProvinceDTO>>(Provinces);
        }

        public async Task<ProvinceDTO?> GetByArabicNameAsync(string ProvinceArabicName)
        {
            var Province = await _ProvinceRepository.GetByArabicNameAsync(ProvinceArabicName);
            if (Province == null) return null;
            return _mapper.Map<ProvinceDTO>(Province);
        }
        
        public async Task<ProvinceDTO?> GetByEnglishNameAsync(string ProvinceEnglishName)
        {
            var Province = await _ProvinceRepository.GetByEnglishNameAsync(ProvinceEnglishName);
            if (Province == null) return null;
            return _mapper.Map<ProvinceDTO>(Province);
        }

        public async Task<ProvinceDTO?> CreateAsync(ProvinceDTO createdProvinceDTO)
        {
            var Province = _mapper.Map<Province>(createdProvinceDTO);
            var createdProvince = await _ProvinceRepository.CreateAsync(Province);
            if (createdProvince == null) return null;
            return _mapper.Map<ProvinceDTO>(createdProvince);
        }

        public async Task<ProvinceDTO?> UpdateAsync(ProvinceDTO ProvinceDto)
        {
            var Province = _mapper.Map<Province>(ProvinceDto);
            var updatedProvince = await _ProvinceRepository.UpdateAsync(Province);
            if (updatedProvince == null) return null;
            return _mapper.Map<ProvinceDTO>(updatedProvince);
        }

        public async Task<bool> DeleteByArabicNameAsync(string ProvinceArabicName)
        {
            return await _ProvinceRepository.DeleteByArabicNameAsync(ProvinceArabicName);
        }

        public async Task<bool> DeleteByEnglishNameAsync(string ProvinceEnglishName)
        {
            return await _ProvinceRepository.DeleteByEnglishNameAsync(ProvinceEnglishName);
        }
    }
}
