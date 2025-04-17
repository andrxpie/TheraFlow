using AutoMapper;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Models;
using System.Net;

namespace BLL.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Appointment> _appointmentsRepository;

        public AppointmentsService(IMapper _mapper, IRepository<Appointment> _appointmentsRepository)
        {
            this._mapper = _mapper;
            this._appointmentsRepository = _appointmentsRepository;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
        {
            try
            {
                var appointment = await _appointmentsRepository.GetAll();
                return _mapper.Map<IEnumerable<AppointmentDto>>(appointment);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting appointment", ex);
            }
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(int id)
        {
            try
            {
                if (id < 0) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                var appointment = await _appointmentsRepository.GetById(id);
                if (appointment == null) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                return _mapper.Map<AppointmentDto>(appointment);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting appointment by id", ex);
            }
        }

        public async Task AddAppointmentAsync(AppointmentDto appointment)
        {
            try
            {
                var appointmentToInsert = _mapper.Map<Appointment>(appointment);
                _appointmentsRepository.Insert(appointmentToInsert);
                await _appointmentsRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding appointment", ex);
            }
        }

        public async Task UpdateAppointmentAsync(AppointmentDto specialist)
        {
            try
            {
                var appointmentToInsert = _mapper.Map<Appointment>(specialist);
                _appointmentsRepository.Update(appointmentToInsert);
                await _appointmentsRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating appointment", ex);
            }
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            if (await GetAppointmentByIdAsync(id) == null) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

            try
            {
                _appointmentsRepository.Delete(id);
                await _appointmentsRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting appointment", ex);
            }
        }
    }
}
