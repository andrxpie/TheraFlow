using Microsoft.AspNetCore.Identity;

namespace BLL.Entities
{
    public enum UserType
    {
        Admin,
        Client,
        Specialist
    }

    public class User : IdentityUser
    {
        public int UserType { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public ICollection<Appointment>? AppointmentsAsClient { get; set; }
        public ICollection<Appointment>? AppointmentsAsSpecialist { get; set; }
        public ICollection<Speciality>? Specialities { get; set; }
    }
}
