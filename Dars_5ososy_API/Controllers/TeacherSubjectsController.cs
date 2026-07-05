using Asp.Versioning;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/teacher-subjects")]
    [ApiController]
    public class TeacherSubjectsController : ControllerBase
    {
        private readonly TeacherSubjectService _teacherSubjectService;

        public TeacherSubjectsController(TeacherSubjectService teacherSubjectService)
        {
            _teacherSubjectService = teacherSubjectService;
        }

        /// <summary>Get all teacher subjects.</summary>
        /// <response code="200">Teacher subjects retrieved successfully.</response>
        /// <response code="404">No teacher subjects found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiResponse<List<TeacherSubjectDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTeacherSubjects()
        {
            var teacherSubjects = await _teacherSubjectService.GetAllAsync();
            if (teacherSubjects == null || !teacherSubjects.Any())
                return NotFound(ApiResponse<object>.Fail("No teacher subjects found."));
            return Ok(ApiResponse<List<TeacherSubjectDTO>>.Successed(teacherSubjects, "Teacher subjects retrieved successfully."));
        }

        /// <summary>Get teacher subjects by student username.</summary>
        /// <response code="200">Teacher subjects retrieved successfully.</response>
        /// <response code="404">No teacher subjects found for the specified subject.</response>
        [HttpGet("get-by-subject/{subjectCode}")]
        [ProducesResponseType(typeof(ApiResponse<List<TeacherSubjectDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeacherSubjectsBySubjectCode(string subjectCode)
        {
            var teacherSubjects = await _teacherSubjectService.GetBySubjectCodeAsync(subjectCode);
            if (teacherSubjects == null || !teacherSubjects.Any())
                return NotFound(ApiResponse<object>.Fail("No teacher subjects found for the specified subject."));
            return Ok(ApiResponse<List<TeacherSubjectDTO>>.Successed(teacherSubjects, "Teacher subjects retrieved successfully."));
        }

        /// <summary>Get TeacherSubjects by teacher username.</summary>
        /// <response code="200">TeacherSubjects retrieved successfully.</response>
        /// <response code="404">No TeacherSubjects found for the specified teacher.</response>
        [HttpGet("get-by-teacher/{teacherUsername}")]
        [ProducesResponseType(typeof(ApiResponse<List<TeacherSubjectDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeacherSubjectsByTeacherUsername(string teacherUsername)
        {
            var teacherSubjects = await _teacherSubjectService.GetByTeacherUsernameAsync(teacherUsername);
            if (teacherSubjects == null || !teacherSubjects.Any())
                return NotFound(ApiResponse<object>.Fail("No teacher subjects found for the specified teacher."));
            return Ok(ApiResponse<List<TeacherSubjectDTO>>.Successed(teacherSubjects, "Teacher subjects retrieved successfully."));
        }

        /// <summary>Create a new teacher subject.</summary>
        /// <remarks>Only <c>Authorized users</c> can create a new teacher subject.</remarks>
        /// <response code="201">Teacher subject created successfully.</response>
        /// <response code="400">Failed to create teacher subject.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="409">Teacher subject with the same details already exists.</response>
        [Authorize]
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<TeacherSubjectDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTeacherSubject(TeacherSubjectDTO TeacherSubjectDto)
        {
            var existingTeacherSubject = await _teacherSubjectService.GetByStudentAndTeacherAsync(TeacherSubjectDto.SubjectCode, TeacherSubjectDto.TeacherUsername);
            if (existingTeacherSubject != null)
                return Conflict(ApiResponse<object>.Fail("Teacher subject with the same details already exists."));
            var createdTeacherSubject = await _teacherSubjectService.CreateAsync(TeacherSubjectDto);
            if (createdTeacherSubject == null)
                return BadRequest(ApiResponse<object>.Fail("Failed to create teacher subject."));
            return CreatedAtAction(nameof(GetAllTeacherSubjects), ApiResponse<TeacherSubjectDTO>.Successed(createdTeacherSubject, "Teacher subject created successfully."));
        }

        /// <summary>Delete a teacher subject by student and teacher usernames.</summary>
        /// <remarks>Only <c>Authorized users</c> can delete a teacher subject.</remarks>
        /// <response code="204">Teacher subject deleted successfully.</response>
        /// <response code="400">Failed to delete teacher subject.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="404">TeacherSubject not found.</response>
        [Authorize]
        [HttpDelete("delete/{studentUsername}/{teacherUsername}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTeacherSubject(string studentUsername, string teacherUsername)
        {
            var existingTeacherSubject = await _teacherSubjectService.GetByStudentAndTeacherAsync(studentUsername, teacherUsername);
            if (existingTeacherSubject == null)
                return NotFound(ApiResponse<object>.Fail("Teacher subject not found."));
            var isDeleted = await _teacherSubjectService.DeleteAsync(studentUsername, teacherUsername);
            if (!isDeleted)
                return BadRequest(ApiResponse<object>.Fail("Failed to delete teacher subject."));
            return NoContent();
        }
    }
}
