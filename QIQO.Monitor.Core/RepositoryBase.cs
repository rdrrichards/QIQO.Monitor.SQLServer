using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace QIQO.Monitor.Core
{
    public abstract class RepositoryBase<T> : IRepository<T> //, IMapper<T> 
        where T : class, IEntity, new()
    {
        protected readonly IMapper<T> Mapper;
        protected readonly ILogger<T> Log;

        public RepositoryBase(ILogger<T> logger, IMapper<T> map)
        {
            Log = logger;
            Mapper = map;
        }

        // What steps would we need to take in order to make this happen?
        //  Mapper<T> - which could be tricky since we want to avoid using reflection
        //  We could use a mapper factory and get the mapper class from the container
        //          This would entail creating a mapper interface, which shouldn't be difficult
        protected IEnumerable<T> MapRows(DbDataReader dr)
        {
            if (!dr.IsClosed)
                try
                {
                    var rows = new List<T>();
                    while (dr.Read())
                        rows.Add(Mapper.Map(dr));
                    dr.Dispose();
                    return rows;
                }
                catch (Exception ex)
                {
                    Log.LogError(ex.Message);
                    Log.LogError(ex.StackTrace);
                    throw;
                }
            else return new List<T>();
        }

        protected T MapRow(DbDataReader dr)
        {
            if (!dr.IsClosed)
                try
                {
                    if (dr.Read())
                        return Mapper.Map(dr);
                    else
                        return new T();
                }
                catch (Exception ex)
                {
                    Log.LogError(ex.Message);
                    Log.LogError(ex.StackTrace);
                    throw;
                }
            else return new T();
        }

        public abstract void Delete(T entity);
        public abstract void DeleteByCode(string entity_code);
        public abstract void DeleteByID(int entity_key);
        public abstract IEnumerable<T> GetAll();
        public abstract T GetByCode(string account_code, string entity_code);
        public abstract T GetByID(int entity_key);
        public abstract void Insert(T entity);
        public abstract void Save(T entity);
    }

}
