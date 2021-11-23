using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.Common.Stores
{
    public interface IEntityStore<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> GetQuery(bool asTracking = true);
    }
}
