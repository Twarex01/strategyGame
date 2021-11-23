using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IQueryable<TEntity> GetQuery(bool asTracking = true)
        {
            var query = dbContext.Set<TEntity>().AsTracking(asTracking ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking);

            return query;
        }
    }
}