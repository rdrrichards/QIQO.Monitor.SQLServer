using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core.Contracts;
using QIQO.Monitor.SQLServer.Data;
using System.Collections.Generic;
using System.Linq;

namespace QIQO.Monitor.Api.Services
{
    public interface IQueryManager
    {
        List<Query> GetQueries();
        List<Query> GetQueries(int monitorKey);
        Query AddQuery(QueryAdd environment);
        Query UpdateQuery(int environmentKey, QueryUpdate environment);
        void DeleteQuery(int environmentKey);
    }
    public class QueryManager : ManagerBase, IQueryManager
    {
        private readonly ICoreCacheService _cacheService;
        private readonly IQueryEntityService _queryEntityService;
        private readonly IQueryRepository _queryRepository;

        public QueryManager(ILogger<QueryManager> logger, ICoreCacheService cacheService,
            IQueryEntityService queryEntityService, IQueryRepository queryRepository) : base(logger)
        {
            _cacheService = cacheService;
            _queryEntityService = queryEntityService;
            _queryRepository = queryRepository;
        }
        public List<Query> GetQueries() => new List<Query>(_queryEntityService.Map(_cacheService.GetQueries().ToList()));
        public List<Query> GetQueries(int monitorKey) => new List<Query>(_queryEntityService.Map(_cacheService.GetQueries(monitorKey).ToList()));
        public Query AddQuery(QueryAdd query)
        {
            return ExecuteOperation(() =>
            {
                var endData = new QueryData
                {
                    Name = query.Name,
                    QueryText = query.QueryText
                };
                _queryRepository.Insert(endData);
                _cacheService.RefreshCache();
                return GetQueries().FirstOrDefault(e => e.Name == query.Name);
            });
        }
        public Query UpdateQuery(int queryKey, QueryUpdate query)
        {
            return ExecuteOperation(() =>
            {
                var endData = new QueryData
                {
                    QueryKey = queryKey,
                    Name = query.Name,
                    QueryText = query.QueryText
                };
                _queryRepository.Save(endData);
                _cacheService.RefreshCache();
                return GetQueries().FirstOrDefault(e => e.QueryKey == queryKey);
            });
        }
        public void DeleteQuery(int queryKey)
        {
            ExecuteOperation(() =>
            {
                var endData = new QueryData { QueryKey = queryKey };
                _queryRepository.Delete(endData);
                _cacheService.RefreshCache();
            });
        }
    }
}
