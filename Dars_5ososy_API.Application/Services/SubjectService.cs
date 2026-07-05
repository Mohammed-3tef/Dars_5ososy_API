using AutoMapper;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.DTOs.UserDTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure.Repositories;

namespace Dars_5ososy_API.Application.Services
{
    public class SubjectService
    {
        private readonly IMapper _mapper;
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<List<SubjectDTO>> GetAllAsync()
        {
            var subjects = await _subjectRepository.GetAllAsync();
            return _mapper.Map<List<SubjectDTO>>(subjects);
        }

        public async Task<SubjectDTO?> GetByNameAsync(string subjectName)
        {
            var subject = await _subjectRepository.GetByNameAsync(subjectName);
            if (subject == null) return null;
            return _mapper.Map<SubjectDTO>(subject);
        }

        public async Task<SubjectDTO?> CreateAsync(SubjectDTO createdSubjectDTO)
        {
            var subject = _mapper.Map<Subject>(createdSubjectDTO);
            var createdSubject = await _subjectRepository.CreateAsync(subject);
            if (createdSubject == null) return null;
            return _mapper.Map<SubjectDTO>(createdSubject);
        }

        public async Task<SubjectDTO?> UpdateAsync(SubjectDTO subjectDto)
        {
            var subject = _mapper.Map<Subject>(subjectDto);
            var updatedSubject = await _subjectRepository.UpdateAsync(subject);
            if (updatedSubject == null) return null;
            return _mapper.Map<SubjectDTO>(updatedSubject);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            return await _subjectRepository.DeleteAsync(id);
        }
    }
}
