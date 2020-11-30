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
     
        public ReadRepositoryBase(IReadMapper<T> map)
        {
            Mapper = map;
        }
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
                catch (Exception)
                {
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
                catch (Exception)
                {
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
