using AutoMapper;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure.Repositories;

namespace Dars_5ososy_API.Application.Services
{
    public class FavoriteService
    {
        private readonly IMapper _mapper;
        private readonly IFavoriteRepository _FavoriteRepository;

        public FavoriteService(IFavoriteRepository FavoriteRepository, IMapper mapper)
        {
            _FavoriteRepository = FavoriteRepository;
            _mapper = mapper;
        }

        public async Task<List<FavoriteDTO>> GetAllAsync()
        {
            var Favorites = await _FavoriteRepository.GetAllAsync();
            return _mapper.Map<List<FavoriteDTO>>(Favorites);
        }

        public async Task<FavoriteDTO?> CreateAsync(FavoriteDTO createdFavoriteDTO)
        {
            var Favorite = _mapper.Map<Favorite>(createdFavoriteDTO);
            var createdFavorite = await _FavoriteRepository.CreateAsync(Favorite);
            if (createdFavorite == null) return null;
            return _mapper.Map<FavoriteDTO>(createdFavorite);
        }

        public async Task<bool> DeleteAsync(string studentUsername, string teacherUsername)
        {
            return await _FavoriteRepository.DeleteAsync(studentUsername, teacherUsername);
        }

        public async Task<List<FavoriteDTO>> GetByTeacherUsernameAsync(string teacherUsername)
        {
            var Favorites = await _FavoriteRepository.GetByTeacherUsernameAsync(teacherUsername);
            if (Favorites == null) return new List<FavoriteDTO>();
            return _mapper.Map<List<FavoriteDTO>>(Favorites);
        }

        public async Task<List<FavoriteDTO?>> GetByStudentUsernameAsync(string studentUsername)
        {
            var Favorites = await _FavoriteRepository.GetByStudentUsernameAsync(studentUsername);
            if (Favorites == null) return new List<FavoriteDTO?>();
            return _mapper.Map<List<FavoriteDTO?>>(Favorites);
        }

        public async Task<FavoriteDTO?> GetByStudentAndTeacherAsync(string studentUsername, string teacherUsername)
        {
            var Favorite = await _FavoriteRepository.GetByStudentAndTeacherAsync(studentUsername, teacherUsername);
            if (Favorite == null) return null;
            return _mapper.Map<FavoriteDTO>(Favorite);
        }
    }
}
