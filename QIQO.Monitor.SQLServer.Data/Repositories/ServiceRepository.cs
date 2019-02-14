﻿using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class ServiceRepository : RepositoryBase<ServiceData>,
                                     IServiceRepository
    {
        private readonly IMonitorDbContext entityContext;
        public ServiceRepository(IMonitorDbContext dbc, IServiceMap map, ILogger<ServiceData> log) : base(log, map)
        {
            entityContext = dbc;
        }

        public override IEnumerable<ServiceData> GetAll()
        {
            Log.LogInformation("Accessing ServiceRepository GetAll function");
            using (entityContext) return MapRows(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_all"));
        }

        public override ServiceData GetByID(int service_key)
        {
            Log.LogInformation("Accessing ServiceRepository GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@service_key", service_key) };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_get", pcol));
        }

        public override ServiceData GetByCode(string service_code, string entityCode)
        {
            Log.LogInformation("Accessing ServiceRepository GetByCode function");
            var pcol = new List<SqlParameter>() {
                Mapper.BuildParam("@service_code", service_code),
                Mapper.BuildParam("@company_code", entityCode)
            };
            using (entityContext) return MapRow(entityContext.ExecuteProcedureAsSqlDataReader("usp_service_get_c", pcol));
        }

        public override void Insert(ServiceData entity)
        {
            Log.LogInformation("Accessing ServiceRepository Insert function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Save(ServiceData entity)
        {
            Log.LogInformation("Accessing ServiceRepository Save function");
            if (entity != null)
                Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(ServiceData entity)
        {
            Log.LogInformation("Accessing ServiceRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_del", Mapper.MapParamsForDelete(entity));
        }

        public override void DeleteByCode(string entityCode)
        {
            Log.LogInformation("Accessing ServiceRepository DeleteByCode function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@service_code", entityCode) };
            pcol.Add(Mapper.GetOutParam());
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_del_c", pcol);
        }

        public override void DeleteByID(int entityKey)
        {
            Log.LogInformation("Accessing ServiceRepository Delete function");
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_del", Mapper.MapParamsForDelete(entityKey));
        }

        private void Upsert(ServiceData entity)
        {
            using (entityContext) entityContext.ExecuteProcedureNonQuery("usp_service_ups", Mapper.MapParamsForUpsert(entity));
        }
    }
}