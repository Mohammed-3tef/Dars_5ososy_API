using Asp.Versioning;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Application.Services;
using Dars_5ososy_API.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dars_5ososy_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/subjects")]
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
                return NotFound(ApiResponse<object>.Failure("No subjects found."));
            return Ok(ApiResponse<List<SubjectDTO>>.Success(subjects, "Subjects retrieved successfully."));
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
                return NotFound(ApiResponse<object>.Failure("Subject not found."));
            return Ok(ApiResponse<SubjectDTO>.Success(subject, "Subject retrieved successfully."));
        }

        /// <summary>Create a new subject.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can create a subject.</remarks>
        /// <response code="201">Subject created successfully.</response>
        /// <response code="400">Failed to create subject.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="409">Subject with the same name already exists.</response>
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<SubjectDTO>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateSubject(SubjectDTO subjectDto)
        {
            var existingSubject = await _subjectService.GetByNameAsync(subjectDto.Name);
            if (existingSubject != null)
                return Conflict(ApiResponse<object>.Failure("Subject with the same name already exists."));
            var createdSubject = await _subjectService.CreateAsync(subjectDto);
            if (createdSubject == null)
                return BadRequest(ApiResponse<object>.Failure("Failed to create subject."));
            return CreatedAtAction(nameof(GetSubjectByName), new { subjectName = createdSubject.Name }, ApiResponse<SubjectDTO>.Success(createdSubject, "Subject created successfully."));
        }

        /// <summary>Update an existing subject.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can update a subject.</remarks>
        /// <response code="200">Subject updated successfully.</response>
        /// <response code="400">Failed to update subject.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        /// <response code="404">Subject not found.</response>
        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ApiResponse<SubjectDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSubject(SubjectDTO subjectDto)
        {
            var existingSubject = await _subjectService.GetByNameAsync(subjectDto.Name);
            if (existingSubject == null)
                return NotFound(ApiResponse<object>.Failure("Subject not found."));
            var updatedSubject = await _subjectService.UpdateAsync(subjectDto);
            if (updatedSubject == null)
                return BadRequest(ApiResponse<object>.Failure("Failed to update subject."));
            return Ok(ApiResponse<SubjectDTO>.Success(updatedSubject, "Subject updated successfully."));
        }

        /// <summary>Create a new favorite.</summary>
        /// <remarks>Only users with the <c>Admin</c> role can delete a subject.</remarks>
        /// <response code="204">Subject deleted successfully.</response>
        /// <response code="400">Failed to delete subject.</response>
        /// <response code="401">Unauthorized. User is not authenticated.</response>
        /// <response code="403">Forbidden. User does not have the required role.</response>
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSubject(long id)
        {
            var isDeleted = await _subjectService.DeleteAsync(id);
            if (!isDeleted)
                return BadRequest(ApiResponse<object>.Failure("Failed to delete subject."));
            return NoContent();
        }
    }
}
