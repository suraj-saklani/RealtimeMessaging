using Microsoft.EntityFrameworkCore;
using RealtimeMessaging.Abstractions.Entities;
using RealtimeMessaging.Core.Repositories;
using RealtimeMessaging.Persistence.DbContexts;
using System.Collections;
using System.Linq.Expressions;

namespace RealtimeMessaging.Persistence.Repositories
{
    internal sealed class Repository<T>
        (RealtimeNotificationsDbContext dbContext) : IRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet = dbContext.Set<T>();
        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> query = _dbSet;

            if (predicate is not null)
                query = query.Where(predicate);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync([id], cancellationToken);
        }

        public async Task<T> AddAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            try
            {

            
            entity.CreatedAt = DateTime.UtcNow;
            entity.Id = Guid.NewGuid();
            await _dbSet.AddAsync(entity, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return entity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<T> UpdateAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            entity.ModifiedAt = DateTime.UtcNow;
            _dbSet.Update(entity);

            await dbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity is null)
                return;

            _dbSet.Remove(entity);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
