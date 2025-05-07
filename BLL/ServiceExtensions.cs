using AutoMapper;
using BLL.Interfaces;
using BLL.Profiles;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class ServiceExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationProfile());
            }).CreateMapper());
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAppointmentsService, AppointmentsService>();
            services.AddScoped<IConsultationNotesService, ConsultationNotesService>();
        }
    }
}
