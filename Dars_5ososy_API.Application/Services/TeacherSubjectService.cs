using AutoMapper;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure.Repositories;

namespace Dars_5ososy_API.Application.Services
{
    public class TeacherSubjectService
    {
        private readonly IMapper _mapper;
        private readonly ITeacherSubjectRepository _TeacherSubjectRepository;

        public TeacherSubjectService(ITeacherSubjectRepository TeacherSubjectRepository, IMapper mapper)
        {
            _TeacherSubjectRepository = TeacherSubjectRepository;
            _mapper = mapper;
        }

        public async Task<List<TeacherSubjectDTO>> GetAllAsync()
        {
            var TeacherSubjects = await _TeacherSubjectRepository.GetAllAsync();
            return _mapper.Map<List<TeacherSubjectDTO>>(TeacherSubjects);
        }

        public async Task<TeacherSubjectDTO?> CreateAsync(TeacherSubjectDTO createdTeacherSubjectDTO)
        {
            var TeacherSubject = _mapper.Map<TeacherSubject>(createdTeacherSubjectDTO);
            var createdTeacherSubject = await _TeacherSubjectRepository.CreateAsync(TeacherSubject);
            if (createdTeacherSubject == null) return null;
            return _mapper.Map<TeacherSubjectDTO>(createdTeacherSubject);
        }

        public async Task<bool> DeleteAsync(string studentUsername, string teacherUsername)
        {
            return await _TeacherSubjectRepository.DeleteAsync(studentUsername, teacherUsername);
        }

        public async Task<List<TeacherSubjectDTO>> GetByTeacherUsernameAsync(string teacherUsername)
        {
            var TeacherSubjects = await _TeacherSubjectRepository.GetByTeacherUsernameAsync(teacherUsername);
            if (TeacherSubjects == null) return new List<TeacherSubjectDTO>();
            return _mapper.Map<List<TeacherSubjectDTO>>(TeacherSubjects);
        }

        public async Task<List<TeacherSubjectDTO>> GetBySubjectCodeAsync(string subjectCode)
        {
            var TeacherSubjects = await _TeacherSubjectRepository.GetBySubjectCodeAsync(subjectCode);
            if (TeacherSubjects == null) return new List<TeacherSubjectDTO>();
            return _mapper.Map<List<TeacherSubjectDTO>>(TeacherSubjects);
        }

        public async Task<TeacherSubjectDTO?> GetByStudentAndTeacherAsync(string studentUsername, string teacherUsername)
        {
            var TeacherSubject = await _TeacherSubjectRepository.GetByStudentAndTeacherAsync(studentUsername, teacherUsername);
            if (TeacherSubject == null) return null;
            return _mapper.Map<TeacherSubjectDTO>(TeacherSubject);
        }
    }
}
