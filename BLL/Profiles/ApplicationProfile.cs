using AutoMapper;
using BLL.Entities;
using BLL.Models;

namespace BLL.Profiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ClientDto, Client>().ReverseMap();
            CreateMap<SpecialistDto, Specialist>().ReverseMap();
            CreateMap<AppointmentDto, Appointment>().ReverseMap();
        }
    }
}
