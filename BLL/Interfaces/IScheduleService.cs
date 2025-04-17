using BLL.Models;

namespace BLL.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDto>> GetAllScheduleAsync();
        Task<ScheduleDto> GetScheduleByIdAsync(int id);
        Task<IEnumerable<ScheduleDto>> GetSchedulesBySpecialistAsync(int specialistId);
        Task AddScheduleAsync(ScheduleDto schedule);
        Task UpdateScheduleAsync(ScheduleDto schedule);
        Task DeleteScheduleAsync(int id);
    }
}
