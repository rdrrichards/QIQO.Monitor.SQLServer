using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Core.Contracts
{
    public interface IMapper
    {
    }

    public interface IMapper<T> : IMapper
    {
        T Map(IDataReader ds);
        List<SqlParameter> MapParamsForUpsert(T entity);
        List<SqlParameter> MapParamsForDelete(T entity);
        List<SqlParameter> MapParamsForDelete(int entity_key);
        SqlParameter GetOutParam();
        SqlParameter BuildParam(string parameterName, object value);
    }

}
