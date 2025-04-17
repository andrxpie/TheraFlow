using AutoMapper;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Models;
using System.Net;

namespace BLL.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Schedule> _scheduleRepository;

        public ScheduleService(IMapper _mapper, IRepository<Schedule> _scheduleRepository)
        {
            this._mapper = _mapper;
            this._scheduleRepository = _scheduleRepository;
        }

        public async Task<IEnumerable<ScheduleDto>> GetAllScheduleAsync()
        {
            try
            {
                var schedules = await _scheduleRepository.GetAll();
                return _mapper.Map<IEnumerable<ScheduleDto>>(schedules);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting schedule", ex);
            }
        }

        public async Task<ScheduleDto> GetScheduleByIdAsync(int id)
        {
            try
            {
                if (id < 0) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                var schedule = await _scheduleRepository.GetById(id);

                if (schedule == null) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                return _mapper.Map<ScheduleDto>(schedule);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting schedule by id", ex);
            }
        }

        public async Task<IEnumerable<ScheduleDto>> GetSchedulesBySpecialistAsync(int specialistId)
        {
            try
            {
                var schedules = await _scheduleRepository.GetAll();
                if (schedules == null) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                var filteredSchedules = schedules.Where(x => x.SpecialistId == specialistId).ToList();
                return filteredSchedules == null
                    ? throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest)
                    : _mapper.Map<IEnumerable<ScheduleDto>>(filteredSchedules);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting schedule", ex);
            }
        }

        public async Task AddScheduleAsync(ScheduleDto schedule)
        {
            try
            {
                var scheduleToInsert = _mapper.Map<Schedule>(schedule);
                _scheduleRepository.Insert(scheduleToInsert);
                await _scheduleRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding schedule", ex);
            }
        }

        public async Task UpdateScheduleAsync(ScheduleDto schedule)
        {
            try
            {
                var scheduleToInsert = _mapper.Map<Schedule>(schedule);
                _scheduleRepository.Update(scheduleToInsert);
                await _scheduleRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating schedule", ex);
            }
        }

        public async Task DeleteScheduleAsync(int id)
        {
            if (await GetScheduleByIdAsync(id) == null) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

            try
            {
                _scheduleRepository.Delete(id);
                await _scheduleRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting schedule", ex);
            }
        }
    }
}
