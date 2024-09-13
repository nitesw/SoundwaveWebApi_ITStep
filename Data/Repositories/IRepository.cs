using Ardalis.Specification;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> GetById(object id);
        Task Insert(TEntity entity);
        Task Delete(object id);
        Task Delete(TEntity entityToDelete);
        Task Update(TEntity entityToUpdate);
        Task Save();

        Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification);
        Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification);

        Task RemoveRange(IEnumerable<TEntity> entities);
    }
}