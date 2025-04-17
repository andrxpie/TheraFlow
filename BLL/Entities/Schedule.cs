namespace BLL.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public int SpecialistId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Specialist Specialist { get; set; } = null!;
    }
}
