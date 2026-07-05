using AutoMapper;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.DTOs.UserDTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure.Repositories;

namespace Dars_5ososy_API.Application.Services
{
    public class EducationSystemService
    {
        private readonly IMapper _mapper;
        private readonly IEducationSystemRepository _educationSystemRepository;

        public EducationSystemService(IEducationSystemRepository educationSystemRepository, IMapper mapper)
        {
            _educationSystemRepository = educationSystemRepository;
            _mapper = mapper;
        }

        public async Task<List<EducationSystemDTO>> GetAllAsync()
        {
            var educationSystems = await _educationSystemRepository.GetAllAsync();
            return _mapper.Map<List<EducationSystemDTO>>(educationSystems);
        }

        public async Task<EducationSystemDTO?> GetByNameAsync(string educationSystemName)
        {
            var educationSystem = await _educationSystemRepository.GetByNameAsync(educationSystemName);
            if (educationSystem == null) return null;
            return _mapper.Map<EducationSystemDTO>(educationSystem);
        }

        public async Task<EducationSystemDTO?> CreateAsync(EducationSystemDTO createdEducationSystemDTO)
        {
            var educationSystem = _mapper.Map<EducationSystem>(createdEducationSystemDTO);
            var createdEducationSystem = await _educationSystemRepository.CreateAsync(educationSystem);
            if (createdEducationSystem == null) return null;
            return _mapper.Map<EducationSystemDTO>(createdEducationSystem);
        }

        public async Task<EducationSystemDTO?> UpdateAsync(EducationSystemDTO educationSystemDto)
        {
            var educationSystem = _mapper.Map<EducationSystem>(educationSystemDto);
            var updatedEducationSystem = await _educationSystemRepository.UpdateAsync(educationSystem);
            if (updatedEducationSystem == null) return null;
            return _mapper.Map<EducationSystemDTO>(updatedEducationSystem);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _educationSystemRepository.DeleteAsync(id);
        }
    }
}
