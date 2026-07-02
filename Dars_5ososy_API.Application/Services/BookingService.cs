using AutoMapper;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure.Repositories;

namespace Dars_5ososy_API.Application.Services
{
    public class BookingService
    {
        private readonly IMapper _mapper;
        private readonly BookingRepository _BookingRepository;

        public BookingService(BookingRepository BookingRepository, IMapper mapper)
        {
            _BookingRepository = BookingRepository;
            _mapper = mapper;
        }

        public async Task<List<BookingDTO>> GetAllAsync()
        {
            var Bookings = await _BookingRepository.GetAllAsync();
            return _mapper.Map<List<BookingDTO>>(Bookings);
        }

        public async Task<BookingDTO?> CreateAsync(BookingDTO createdBookingDTO)
        {
            var Booking = _mapper.Map<Booking>(createdBookingDTO);
            var createdBooking = await _BookingRepository.CreateAsync(Booking);
            if (createdBooking == null) return null;
            return _mapper.Map<BookingDTO>(createdBooking);
        }

        public async Task<bool> DeleteAsync(string studentUsername, string teacherUsername)
        {
            return await _BookingRepository.DeleteAsync(studentUsername, teacherUsername);
        }

        public async Task<List<BookingDTO>> GetByTeacherUsernameAsync(string teacherUsername)
        {
            var Bookings = await _BookingRepository.GetByTeacherUsernameAsync(teacherUsername);
            if (Bookings == null) return new List<BookingDTO>();
            return _mapper.Map<List<BookingDTO>>(Bookings);
        }

        public async Task<List<BookingDTO>> GetByStudentUsernameAsync(string studentUsername)
        {
            var Bookings = await _BookingRepository.GetByStudentUsernameAsync(studentUsername);
            if (Bookings == null) return new List<BookingDTO>();
            return _mapper.Map<List<BookingDTO?>>(Bookings);
        }

        public async Task<BookingDTO?> GetByStudentAndTeacherAsync(string studentUsername, string teacherUsername)
        {
            var Booking = await _BookingRepository.GetByStudentAndTeacherAsync(studentUsername, teacherUsername);
            if (Booking == null) return null;
            return _mapper.Map<BookingDTO>(Booking);
        }
    }
}
