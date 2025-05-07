using BLL.Models;

namespace BLL.Interfaces
{
    public interface IAppointmentsService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task<IEnumerable<AppointmentDto>> GetClientsAppointmentsAsync(string id);
        Task<IEnumerable<AppointmentDto>> GetSpecialistsAppointmentsAsync(string id);
        Task AddAppointmentAsync(AppointmentDto appointment);
        Task UpdateAppointmentAsync(AppointmentDto appointment);
        Task DeleteAppointmentAsync(int id);
    }
}
