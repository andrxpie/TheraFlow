using BLL.Models;

namespace BLL.Interfaces
{
    public interface IAppointmentsService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
        Task<AppointmentDto> GetAppointmentByIdAsync(int id);
        Task<IEnumerable<AppointmentDto>> GetClientsAppointmentsAsync(int clientId);
        Task<IEnumerable<AppointmentDto>> GetSpecialistsAppointmentsAsync(int cpecialistId);
        Task AddAppointmentAsync(AppointmentDto appointment);
        Task UpdateAppointmentAsync(AppointmentDto appointment);
        Task DeleteAppointmentAsync(int id);
    }
}
