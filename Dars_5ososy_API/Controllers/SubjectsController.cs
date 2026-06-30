using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly SubjectService _subjectService;

        public SubjectsController(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllAsync();
            if (subjects == null || !subjects.Any())
                return NotFound(ApiResponse<object>.Fail("No subjects found."));
            return Ok(ApiResponse<List<SubjectDTO>>.Succeeded(subjects, "Subjects retrieved successfully."));
        }

        [HttpGet("get-by-name/{subjectName}")]
        public async Task<IActionResult> GetSubjectByName(string subjectName)
        {
            var subject = await _subjectService.GetByNameAsync(subjectName);

            if (subject == null)
                return NotFound(ApiResponse<object>.Fail("Subject not found."));

            return Ok(ApiResponse<SubjectDTO>.Succeeded(subject, "Subject retrieved successfully."));
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSubject(SubjectDTO subjectDto)
        {
            var createdSubject = await _subjectService.CreateAsync(subjectDto);
            if (createdSubject == null)
                return BadRequest(ApiResponse<object>.Fail("Subject with the same name already exists."));

            return CreatedAtAction(nameof(GetSubjectByName), new { subjectName = createdSubject.Name }, ApiResponse<SubjectDTO>.Succeeded(createdSubject, "Subject created successfully."));
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSubject(SubjectDTO subjectDto)
        {
            var updatedSubject = await _subjectService.UpdateAsync(subjectDto);
            if (updatedSubject == null)
                return NotFound(ApiResponse<object>.Fail("Subject not found."));

            return Ok(ApiResponse<SubjectDTO>.Succeeded(updatedSubject, "Subject updated successfully."));
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSubject(long id)
        {
            var isDeleted = await _subjectService.DeleteAsync(id);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Subject not found."));

            return Ok(ApiResponse<object>.Succeeded(null, "Subject deleted successfully."));
        }
    }
}
