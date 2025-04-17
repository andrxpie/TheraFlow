using AutoMapper;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Models;
using System.Net;

namespace BLL.Services
{
    public class ConsultationNotesService : IConsultationNotesService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ConsultationNote> _consultationNotesRepository;

        public ConsultationNotesService(IMapper _mapper, IRepository<ConsultationNote> _consultationNotesRepository)
        {
            this._mapper = _mapper;
            this._consultationNotesRepository = _consultationNotesRepository;
        }

        public async Task<IEnumerable<ConsultationNoteDto>> GetAllConsultationNotesAsync()
        {
            try
            {
                var consultationNotes = await _consultationNotesRepository.GetAll();
                return _mapper.Map<IEnumerable<ConsultationNoteDto>>(consultationNotes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting consultation notes", ex);
            }
        }

        public async Task<ConsultationNoteDto> GetConsultationNoteByIdAsync(int id)
        {
            try
            {
                if (id < 0) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                var consultationNote = await _consultationNotesRepository.GetById(id);

                return consultationNote == null
                    ? throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest)
                    : _mapper.Map<ConsultationNoteDto>(consultationNote);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting consultation note by id", ex);
            }
        }

        public async Task<IEnumerable<ConsultationNoteDto>> GetConsultationNotesByAppointmentAsync(int appointmentId)
        {
            try
            {
                var consultationNotes = await _consultationNotesRepository.GetAll();
                if (consultationNotes == null) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                var filteredConsultationNotes = consultationNotes.Where(x => x.AppointmentId == appointmentId);
                return filteredConsultationNotes == null
                    ? throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest)
                    : _mapper.Map<IEnumerable<ConsultationNoteDto>>(filteredConsultationNotes);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting consultation notes by appointment", ex);
            }
        }

        public async Task AddConsultationNoteAsync(ConsultationNoteDto consultationNote)
        {
            try
            {
                var consultationNoteToInsert = _mapper.Map<ConsultationNote>(consultationNote);
                _consultationNotesRepository.Insert(consultationNoteToInsert);
                await _consultationNotesRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding consultation note", ex);
            }
        }

        public async Task UpdateConsultationNoteAsync(ConsultationNoteDto consultationNote)
        {
            try
            {
                var consultationNoteToInsert = _mapper.Map<ConsultationNote>(consultationNote);
                _consultationNotesRepository.Update(consultationNoteToInsert);
                await _consultationNotesRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating consultation note", ex);
            }
        }

        public async Task DeleteConsultationNoteAsync(int id)
        {
            if (await GetConsultationNoteByIdAsync(id) == null) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

            try
            {
                _consultationNotesRepository.Delete(id);
                await _consultationNotesRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting consultation note", ex);
            }
        }
    }
}
