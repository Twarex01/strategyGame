using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.Common.Stores
{
    public abstract class EntityStore<TEntity> : IEntityStore<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbContext dbContext;

        protected EntityStore(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TEntity> GetEntity(Guid id, bool asTracking = true, CancellationToken cancellationToken = default)
        {
            return await GetQuery(asTracking).SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public IQueryable<TEntity> GetQuery(bool asTracking = true)
        {
            var query = dbContext.Set<TEntity>().AsTracking(asTracking ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking);

            return query;
        }
    }
}