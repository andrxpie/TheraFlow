using Ardalis.Specification;

namespace BLL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(object id);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);

        public Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification);
        public Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification);

        public Task Save();
    }
}
