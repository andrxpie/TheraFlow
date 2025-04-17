namespace BLL.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int SpecialistId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Client Client { get; set; } = null!;
        public Specialist Specialist { get; set; } = null!;
        public ConsultationNote? Note { get; set; }
    }

}
