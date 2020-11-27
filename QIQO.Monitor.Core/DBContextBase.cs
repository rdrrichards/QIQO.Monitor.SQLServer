using Microsoft.Extensions.Logging;
using QIQO.Monitor.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.Core
{
    public class DbContextBase : IDbContext
    {
        protected readonly SqlConnection _connection;
        protected readonly ILogger<DbContextBase> Log;

        public DbContextBase(ILogger<DbContextBase> logger, string connectionString)
        {
            Log = logger;
            _connection = new SqlConnection(connectionString);
        }

        public virtual void ExecuteProcedureAsReader(string procedureName, IEnumerable<SqlParameter> parameters) => throw new NotImplementedException();

        public virtual int ExecuteProcedureNonQuery(string procedureName, IEnumerable<SqlParameter> parameters)
        {
            var cmd = new SqlCommand(procedureName, _connection) { CommandType = CommandType.StoredProcedure };
            int ret_val;

            foreach (var sparam in parameters)
                cmd.Parameters.Add(BuildParameter(sparam));

            try
            {
                _connection.Open();
                ret_val = cmd.ExecuteNonQuery();
                _connection.Close();
                return ret_val;
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
                throw;
            }
            finally
            {
                cmd.Dispose();
                _connection.Close();
            }

        }

        public virtual int ExecuteNonQuerySQLStatement(string sqlStatement)
        {
            int ret_val;
            var cmd = new SqlCommand(sqlStatement, _connection) { CommandType = CommandType.Text };

            try
            {
                _connection.Open();
                ret_val = cmd.ExecuteNonQuery();
                _connection.Close();
                return ret_val;
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
                throw;
            }
            finally
            {
                cmd.Dispose();
                _connection.Close();
            }
        }

        public virtual int ExecuteNonQuerySQLStatement(string sqlStatement, IEnumerable<SqlParameter> parameters) => throw new NotImplementedException();
        public virtual T ExecuteSqlStatementAsScalar<T>(string sqlStatement) => throw new NotImplementedException();

        public virtual T ExecuteSqlStatementAsScalar<T>(string sqlStatement, IEnumerable<SqlParameter> parameters)
        {
            var cmd = new SqlCommand(sqlStatement, _connection) { CommandType = CommandType.Text };

            foreach (var sparam in parameters)
                cmd.Parameters.Add(BuildParameter(sparam));

            T ret_val;
            try
            {
                _connection.Open();
                ret_val = (T)cmd.ExecuteScalar();
                _connection.Close();
                return ret_val;
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
                throw;
            }
            finally
            {
                cmd.Dispose();
                _connection.Close();
            }
        }
        public virtual void Dispose() => _connection.Close();

        protected SqlParameter BuildParameter(SqlParameter sparam)
        {
            return new SqlParameter()
            {
                ParameterName = sparam.ParameterName,
                Value = sparam.Value,
                DbType = sparam.DbType,
                Direction = sparam.Direction,
                TypeName = sparam.TypeName
            };
        }

        public SqlDataReader ExecuteProcedureAsSqlDataReader(string procedureName, IEnumerable<SqlParameter> parameters)
        {
            var cmd = new SqlCommand(procedureName, _connection) { CommandType = CommandType.StoredProcedure };

            foreach (var sparam in parameters)
                cmd.Parameters.Add(BuildParameter(sparam));

            try
            {
                _connection.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
                throw;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        public SqlDataReader ExecuteProcedureAsSqlDataReader(string procedureName)
        {
            var cmd = new SqlCommand(procedureName, _connection) { CommandType = CommandType.StoredProcedure };

            try
            {
                _connection.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
                throw;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        public SqlDataReader ExecuteSqlStatementAsSqlDataReader(string sqlStatement)
        {
            var cmd = new SqlCommand(sqlStatement, _connection) { CommandType = CommandType.Text };

            try
            {
                _connection.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
                throw;
            }
            finally
            {
                cmd.Dispose();
            }
        }

    }

}
