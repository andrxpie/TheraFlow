namespace BLL.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public User Client { get; set; } = null!;
        public string SpecialistId { get; set; }
        public User Specialist { get; set; } = null!;
        public DateTime AppointmentDate { get; set; }
        public ConsultationNote? Note { get; set; }
    }
}
