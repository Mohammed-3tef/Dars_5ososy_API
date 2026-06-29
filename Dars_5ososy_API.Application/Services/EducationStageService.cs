using AutoMapper;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.DTOs.UserDTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure.Repositories;

namespace Dars_5ososy_API.Application.Services
{
    public class EducationStageService
    {
        private readonly IMapper _mapper;
        private readonly EducationStageRepository _EducationStageRepository;

        public EducationStageService(EducationStageRepository EducationStageRepository, IMapper mapper)
        {
            _EducationStageRepository = EducationStageRepository;
            _mapper = mapper;
        }

        public async Task<List<EducationStageDTO>> GetAllAsync()
        {
            var educationStages = await _EducationStageRepository.GetAllAsync();
            return _mapper.Map<List<EducationStageDTO>>(educationStages);
        }

        public async Task<EducationStageDTO?> GetByNameAsync(string educationStageName)
        {
            var educationStage = await _EducationStageRepository.GetByNameAsync(educationStageName);
            if (educationStage == null) return null;
            return _mapper.Map<EducationStageDTO>(educationStage);
        }

        public async Task<EducationStageDTO?> CreateAsync(EducationStageDTO createdEducationStageDTO)
        {
            var educationStage = _mapper.Map<EducationStage>(createdEducationStageDTO);
            var createdEducationStage = await _EducationStageRepository.CreateAsync(educationStage);
            if (createdEducationStage == null) return null;
            return _mapper.Map<EducationStageDTO>(createdEducationStage);
        }

        public async Task<EducationStageDTO?> UpdateAsync(EducationStageDTO educationStageDto)
        {
            var educationStage = _mapper.Map<EducationStage>(educationStageDto);
            var updatedEducationStage = await _EducationStageRepository.UpdateAsync(educationStage);
            if (updatedEducationStage == null) return null;
            return _mapper.Map<EducationStageDTO>(updatedEducationStage);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _EducationStageRepository.DeleteAsync(id);
        }
    }
}
