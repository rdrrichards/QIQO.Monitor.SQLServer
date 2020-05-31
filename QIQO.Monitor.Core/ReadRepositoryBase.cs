using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace QIQO.Monitor.Core
{
    public abstract class ReadRepositoryBase<T> : IReadRepository<T> //, IMapper<T> 
        where T : class, IEntity, new()
    {
        protected readonly IReadMapper<T> Mapper;
        protected readonly ILogger<T> Log;

        public ReadRepositoryBase(ILogger<T> logger, IReadMapper<T> map)
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
                    // dr.Dispose();
                    return rows;
                }
                catch (Exception ex)
                {
                    Log.LogError(ex.StackTrace);
                    throw;
                }
                finally
                {
                    dr.Dispose();
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
                    Log.LogError(ex.StackTrace);
                    throw;
                }
                finally
                {
                    dr.Dispose();
                }
            else return new T();
        }

        public abstract IEnumerable<T> Get();
    }
}
