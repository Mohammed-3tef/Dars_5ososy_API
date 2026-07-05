using AutoMapper;
using Dars_5ososy_API.Application.DTOs;
using Dars_5ososy_API.Domain.Entities;
using Dars_5ososy_API.Infrastructure.Repositories;

namespace Dars_5ososy_API.Application.Services
{
    public class AvailabilitySlotService
    {
        private readonly IMapper _mapper;
        private readonly IAvailabilitySlotRepository _availabilitySlotRepository;

        public AvailabilitySlotService(IAvailabilitySlotRepository availabilitySlotRepository, IMapper mapper)
        {
            _mapper = mapper;
            _availabilitySlotRepository = availabilitySlotRepository;
        }

        public async Task<List<AvailabilitySlotDTO>> GetAllAvailabilitySlotsAsync()
        {
            var availabilitySlots = await _availabilitySlotRepository.GetAllAsync();
            return _mapper.Map<List<AvailabilitySlotDTO>>(availabilitySlots);
        }

        public async Task<List<AvailabilitySlotDTO>> GetAvailabilitySlotsByTeacherUsernameAsync(string teacherUsername)
        {
            var availabilitySlots = await _availabilitySlotRepository.GetAllByUsernameAsync(teacherUsername);
            return _mapper.Map<List<AvailabilitySlotDTO>>(availabilitySlots);
        }

        public async Task<AvailabilitySlotDTO> GetSpecificAvailabilitySlotByTeacherUsernameAsync(string teacherUsername, string DayOfWeek, TimeOnly startTime)
        {
            var availabilitySlot = await _availabilitySlotRepository.GetAllByUsernameAsync(teacherUsername);
            var filteredAvailabilitySlot = availabilitySlot.FirstOrDefault(slot => slot.DayOfWeek.ToString() == DayOfWeek && slot.StartTime == startTime);
            if (filteredAvailabilitySlot == null)
                return null;
            return _mapper.Map<AvailabilitySlotDTO>(filteredAvailabilitySlot);
        }

        public async Task<AvailabilitySlotDTO> CreateAvailabilitySlotAsync(AvailabilitySlotDTO availabilitySlotDTO)
        {
            var availabilitySlot = _mapper.Map<AvailabilitySlot>(availabilitySlotDTO);
            var createdAvailabilitySlot = await _availabilitySlotRepository.CreateAsync(availabilitySlot);
            if (createdAvailabilitySlot == null)
                return null;
            return _mapper.Map<AvailabilitySlotDTO>(createdAvailabilitySlot);
        }

        public async Task<AvailabilitySlotDTO> UpdateAvailabilitySlotAsync(AvailabilitySlotDTO availabilitySlotDTO)
        {
            var availabilitySlot = _mapper.Map<AvailabilitySlot>(availabilitySlotDTO);
            var updatedAvailabilitySlot = await _availabilitySlotRepository.UpdateAsync(availabilitySlot);
            if (updatedAvailabilitySlot == null)
                return null;
            return _mapper.Map<AvailabilitySlotDTO>(updatedAvailabilitySlot);
        }

        public async Task<bool> DeleteAvailabilitySlotAsync(string teacherUsername, string DayOfWeek, TimeOnly startTime)
        {
            var availabilitySlot = await _availabilitySlotRepository.GetAllByUsernameAsync(teacherUsername);
            var filteredAvailabilitySlot = availabilitySlot.FirstOrDefault(slot => slot.DayOfWeek.ToString() == DayOfWeek && slot.StartTime == startTime);
            if (filteredAvailabilitySlot == null)
                return false;
            return await _availabilitySlotRepository.DeleteAsync(filteredAvailabilitySlot.Id);
        }
    }
}
