using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.Common.Stores
{
    public interface IEntityStore<TEntity> where TEntity : IEntity
    {
        Task<TEntity> GetEntity(Guid id, bool asTracking = true, CancellationToken cancellationToken = default);

        IQueryable<TEntity> GetQuery(bool asTracking = true);

        void Add(TEntity entity);

        Task<int> SaveChanges(CancellationToken cancellationToken = default);
    }
}
