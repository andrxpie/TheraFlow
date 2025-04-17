using BLL.Models;

namespace BLL.Interfaces
{
    public interface ISpecialistService
    {
        Task<IEnumerable<SpecialistDto>> GetAllSpecialistsAsync();
        Task<SpecialistDto> GetSpecialistByIdAsync(int id);
        Task AddSpecialistAsync(SpecialistDto specialist);
        Task UpdateSpecialistAsync(SpecialistDto specialist);
        Task DeleteSpecialistAsync(int id);
    }
}
