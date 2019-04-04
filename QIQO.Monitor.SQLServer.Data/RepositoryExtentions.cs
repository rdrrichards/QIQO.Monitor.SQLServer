using QIQO.Monitor.Core.Contracts;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public static class RepositoryExtentions
    {
        public static void InsertAll<T>(this IRepository<T> repo, IEnumerable<T> entities) where T : class, IEntity, new()
        {
            foreach (var ent in entities)
                repo.Insert(ent);
        }
        public static void DeleteAll<T>(this IRepository<T> repo, IEnumerable<T> entities) where T : class, IEntity, new()
        {
            foreach (var ent in entities)
                repo.Delete(ent);
        }
    }
}
