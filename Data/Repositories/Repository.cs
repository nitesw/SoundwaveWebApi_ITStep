using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal SoundwaveDbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(SoundwaveDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public Task Save() => context.SaveChangesAsync();

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task Delete(object id)
        {
            TEntity? entityToDelete = await dbSet.FindAsync(id);
            if (entityToDelete != null)
                await Delete(entityToDelete);
        }

        public Task Delete(TEntity entityToDelete)
        {
            return Task.Run(() =>
            {
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            });
        }

        public Task Update(TEntity entityToUpdate)
        {
            return Task.Run(() =>
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
            });
        }

        public async Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(dbSet, specification);
        }

        public Task RemoveRange(IEnumerable<TEntity> entities)
        {
            return Task.Run(() =>
            {
                dbSet.RemoveRange(entities);
            });
        }

    }
}
