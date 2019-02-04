using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Core.Contracts
{
    public static class EntityServiceExtensions
    {

        public static List<TEntity> Map<TModel, TEntity>(this IEntityService<TModel, TEntity> svc, IEnumerable<TModel> entities)
            where TModel : IModel
            where TEntity : IEntity
        {
            var maps = new List<TEntity>();
            entities.ToList().ForEach(ent => maps.Add(svc.Map(ent)));
            return maps;
        }

        public static List<TModel> Map<TModel, TEntity>(this IEntityService<TModel, TEntity> svc, IEnumerable<TEntity> entities)
            where TModel : IModel
            where TEntity : IEntity
        {
            var maps = new List<TModel>();
            entities.ToList().ForEach(ent => maps.Add(svc.Map(ent)));
            return maps;
        }
    }
}
