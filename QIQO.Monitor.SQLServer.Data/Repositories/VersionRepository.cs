﻿using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core;
using System;
using System.Collections.Generic;

namespace QIQO.Monitor.SQLServer.Data
{
    public class VersionRepository : RepositoryBase<VersionData>,
                                     IVersionRepository
    {
        private readonly ISqlServerDbContext entityContext;
        public VersionRepository(ISqlServerDbContext dbc, IVersionMap map, ILogger<VersionData> log) : base(log, map)
        {
            entityContext = dbc;
        }
        public VersionData Get()
        {
            Log.LogInformation("Accessing VersionRepository Get function");
            using (entityContext) return MapRow(entityContext.ExecuteSqlStatementAsSqlDataReader("SELECT @@VERSION AS version_text"));
        }

        public override IEnumerable<VersionData> GetAll() => throw new NotImplementedException();

        public override VersionData GetByID(int server_key) => throw new NotImplementedException();

        public override VersionData GetByCode(string server_code, string entityCode = "") => throw new NotImplementedException();

        public override void Insert(VersionData entity) => throw new NotImplementedException();

        public override void Save(VersionData entity) => throw new NotImplementedException();

        public override void Delete(VersionData entity) => throw new NotImplementedException();

        public override void DeleteByCode(string entityCode) => throw new NotImplementedException();

        public override void DeleteByID(int entityKey) => throw new NotImplementedException();
    }
}
