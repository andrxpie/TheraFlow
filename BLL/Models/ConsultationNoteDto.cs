namespace BLL.Models
{
    public class ConsultationNoteDto
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string Notes { get; set; }
    }
}
