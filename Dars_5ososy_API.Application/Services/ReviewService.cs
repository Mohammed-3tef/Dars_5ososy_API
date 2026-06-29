using AutoMapper;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure.Repositories;

namespace Dars_5ososy_API.Application.Services
{
    public class ReviewService
    {
        private readonly IMapper _mapper;
        private readonly ReviewRepository _ReviewRepository;

        public ReviewService(ReviewRepository ReviewRepository, IMapper mapper)
        {
            _ReviewRepository = ReviewRepository;
            _mapper = mapper;
        }

        public async Task<List<ReviewDTO>> GetAllAsync()
        {
            var Reviews = await _ReviewRepository.GetAllAsync();
            return _mapper.Map<List<ReviewDTO>>(Reviews);
        }

        public async Task<ReviewDTO?> CreateAsync(ReviewDTO createdReviewDTO)
        {
            var Review = _mapper.Map<Review>(createdReviewDTO);
            var createdReview = await _ReviewRepository.CreateAsync(Review);
            if (createdReview == null) return null;
            return _mapper.Map<ReviewDTO>(createdReview);
        }

        public async Task<ReviewDTO?> UpdateAsync(ReviewDTO ReviewDto)
        {
            var Review = _mapper.Map<Review>(ReviewDto);
            var updatedReview = await _ReviewRepository.UpdateAsync(Review);
            if (updatedReview == null) return null;
            return _mapper.Map<ReviewDTO>(updatedReview);
        }

        public async Task<bool> DeleteAsync(string studentUsername, string teacherUsername)
        {
            return await _ReviewRepository.DeleteAsync(studentUsername, teacherUsername);
        }

        public async Task<List<ReviewDTO>> GetByTeacherUsernameAsync(string teacherUsername)
        {
            var reviews = await _ReviewRepository.GetByTeacherUsernameAsync(teacherUsername);
            if (reviews == null) return new List<ReviewDTO>();
            return _mapper.Map<List<ReviewDTO>>(reviews);
        }

        public async Task<List<ReviewDTO>> GetByStudentUsernameAsync(string studentUsername)
        {
            var reviews = await _ReviewRepository.GetByStudentUsernameAsync(studentUsername);
            if (reviews == null) return new List<ReviewDTO>();
            return _mapper.Map<List<ReviewDTO?>>(reviews);
        }

        public async Task<ReviewDTO?> GetByStudentAndTeacherAsync(string studentUsername, string teacherUsername)
        {
            var review = await _ReviewRepository.GetByStudentAndTeacherAsync(studentUsername, teacherUsername);
            if (review == null) return null;
            return _mapper.Map<ReviewDTO>(review);
        }
    }
}
