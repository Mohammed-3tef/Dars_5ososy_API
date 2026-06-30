using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly SubjectService _subjectService;

        public SubjectsController(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        /// <summary>Get all subjects.</summary>
        /// <response code="200">Subjects retrieved successfully.</response>
        /// <response code="404">No subjects found.</response>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(ApiResponse<List<SubjectDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await _subjectService.GetAllAsync();
            if (subjects == null || !subjects.Any())
                return NotFound(ApiResponse<object>.Fail("No subjects found."));
            return Ok(ApiResponse<List<SubjectDTO>>.Succeeded(subjects, "Subjects retrieved successfully."));
        }

        /// <summary>Get a subject by name.</summary>
        /// <response code="200">Subject retrieved successfully.</response>
        /// <response code="404">Subject not found.</response>
        [HttpGet("get-by-name/{subjectName}")]
        [ProducesResponseType(typeof(ApiResponse<SubjectDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubjectByName(string subjectName)
        {
            var subject = await _subjectService.GetByNameAsync(subjectName);
            if (subject == null)
                return NotFound(ApiResponse<object>.Fail("Subject not found."));
            return Ok(ApiResponse<SubjectDTO>.Succeeded(subject, "Subject retrieved successfully."));
        }

        /// <summary>Create a new subject.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can create a subject.</remarks>
        /// <response code="201">Subject created successfully.</response>
        /// <response code="400">Subject with the same name already exists.</response>
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<SubjectDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateSubject(SubjectDTO subjectDto)
        {
            var createdSubject = await _subjectService.CreateAsync(subjectDto);
            if (createdSubject == null)
                return BadRequest(ApiResponse<object>.Fail("Subject with the same name already exists."));
            return CreatedAtAction(nameof(GetSubjectByName), new { subjectName = createdSubject.Name }, ApiResponse<SubjectDTO>.Succeeded(createdSubject, "Subject created successfully."));
        }

        /// <summary>Update an existing subject.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can update a subject.</remarks>
        /// <response code="200">Subject updated successfully.</response>
        /// <response code="404">Subject not found.</response>
        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<SubjectDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSubject(SubjectDTO subjectDto)
        {
            var updatedSubject = await _subjectService.UpdateAsync(subjectDto);
            if (updatedSubject == null)
                return NotFound(ApiResponse<object>.Fail("Subject not found."));
            return Ok(ApiResponse<SubjectDTO>.Succeeded(updatedSubject, "Subject updated successfully."));
        }

        /// <summary>Create a new favorite.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can delete a subject.</remarks>
        /// <response code="200">Subject deleted successfully.</response>
        /// <response code="404">Subject not found.</response>
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSubject(long id)
        {
            var isDeleted = await _subjectService.DeleteAsync(id);
            if (!isDeleted)
                return NotFound(ApiResponse<object>.Fail("Subject not found."));
            return Ok(ApiResponse<object>.Succeeded(null, "Subject deleted successfully."));
        }
    }
}
