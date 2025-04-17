namespace BLL.Models
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int SpecialistId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
