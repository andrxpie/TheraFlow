using AutoMapper;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Models;
using System.Net;

namespace BLL.Services
{
    public class SpecialistService : ISpecialistService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Specialist> _specialistRepository;

        public SpecialistService(IMapper _mapper, IRepository<Specialist> _specialistRepository)
        {
            this._mapper = _mapper;
            this._specialistRepository = _specialistRepository;
        }

        public async Task<IEnumerable<SpecialistDto>> GetAllSpecialistsAsync()
        {
            try
            {
                var specialist = await _specialistRepository.GetAll();
                return _mapper.Map<IEnumerable<SpecialistDto>>(specialist);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting specialists", ex);
            }
        }

        public async Task<SpecialistDto> GetSpecialistByIdAsync(int id)
        {
            try
            {
                if (id < 0) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                var specialist = await _specialistRepository.GetById(id);
                if (specialist == null) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

                return _mapper.Map<SpecialistDto>(specialist);
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting specialist by id", ex);
            }
        }

        public async Task AddSpecialistAsync(SpecialistDto specialist)
        {
            try
            {
                var specialistToInsert = _mapper.Map<Specialist>(specialist);
                _specialistRepository.Insert(specialistToInsert);
                await _specialistRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding specialist", ex);
            }
        }

        public async Task UpdateSpecialistAsync(SpecialistDto specialist)
        {
            try
            {
                var specialistToInsert = _mapper.Map<Specialist>(specialist);
                _specialistRepository.Update(specialistToInsert);
                await _specialistRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating specialist", ex);
            }
        }

        public async Task DeleteSpecialistAsync(int id)
        {
            if (await GetSpecialistByIdAsync(id) == null) throw new HttpException(Errors.ItemNotFound, HttpStatusCode.BadRequest);

            try
            {
                _specialistRepository.Delete(id);
                await _specialistRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting specialist", ex);
            }
        }
    }
}
