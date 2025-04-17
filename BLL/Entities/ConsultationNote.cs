namespace BLL.Entities
{
    public class ConsultationNote
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string Notes { get; set; } = null!;

        public Appointment Appointment { get; set; } = null!;
    }
}
