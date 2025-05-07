namespace BLL.Models
{
    public class UserDto
    {
        public int UserType { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public ICollection<SpecialityDto>? Specialities { get; set; }
    }
}
