namespace BLL.Models
{
    public class ScheduleDto
    {
        public int Id { get; set; }
        public int SpecialistId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
