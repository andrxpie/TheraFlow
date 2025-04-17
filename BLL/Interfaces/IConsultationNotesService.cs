using BLL.Models;

namespace BLL.Interfaces
{
    public interface IConsultationNotesService
    {
        Task<IEnumerable<ConsultationNoteDto>> GetAllConsultationNotesAsync();
        Task<ConsultationNoteDto> GetConsultationNoteByIdAsync(int id);
        Task<IEnumerable<ConsultationNoteDto>> GetConsultationNotesByAppointmentAsync(int specialistId);
        Task AddConsultationNoteAsync(ConsultationNoteDto consultationNote);
        Task UpdateConsultationNoteAsync(ConsultationNoteDto consultationNote);
        Task DeleteConsultationNoteAsync(int id);
    }
}
