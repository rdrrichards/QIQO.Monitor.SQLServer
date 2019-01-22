using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Monitor.Core.Contracts
{
    public interface IDbContext : IDisposable
    {
        void ExecuteProcedureAsReader(string procedureName, IEnumerable<SqlParameter> parameters);
        int ExecuteProcedureNonQuery(string procedureName, IEnumerable<SqlParameter> parameters);

        int ExecuteNonQuerySQLStatement(string sqlStatement);
        int ExecuteNonQuerySQLStatement(string sqlStatement, IEnumerable<SqlParameter> parameters);

        SqlDataReader ExecuteProcedureAsSqlDataReader(string sqlStatement);
        SqlDataReader ExecuteProcedureAsSqlDataReader(string sqlStatement, IEnumerable<SqlParameter> parameters);

        T ExecuteSqlStatementAsScalar<T>(string sqlStatement);
        T ExecuteSqlStatementAsScalar<T>(string sqlStatement, IEnumerable<SqlParameter> parameters);
        SqlDataReader ExecuteSqlStatementAsSqlDataReader(string sqlStatement);
    }
}
