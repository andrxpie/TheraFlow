namespace BLL.Entities
{
    public class Specialist
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Specialty { get; set; } = null!;
        public string Email { get; set; } = null!;

        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
