using AutoMapper;
using BLL.Entities;
using BLL.Models;

namespace BLL.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegisterAdminDto>().ReverseMap();
            CreateMap<User, RegisterDefaultUserDto>().ReverseMap();
            CreateMap<Speciality, SpecialityDto>().ReverseMap();
            CreateMap<AppointmentDto, Appointment>().ReverseMap();
            CreateMap<ConsultationNoteDto, ConsultationNote>().ReverseMap();
        }
    }
}
