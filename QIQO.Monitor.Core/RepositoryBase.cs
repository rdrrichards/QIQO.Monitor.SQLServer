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

        public RepositoryBase(IMapper<T> map)
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

        public abstract void Delete(T entity);
        public abstract void DeleteByID(int entity_key);
        public abstract IEnumerable<T> GetAll();
        public abstract T GetByID(int entity_key);
        public abstract void Insert(T entity);
        public abstract void Save(T entity);
    }
}
