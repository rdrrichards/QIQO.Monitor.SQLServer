using System;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Monitor.SQLServer.Data
{
    public class MapperBase
    {
        public SqlParameter GetOutParam()
        {
            return new SqlParameter()
            {
                ParameterName = "@key",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
        }

        public SqlParameter GetIdentityOutParam()
        {
            // for the output param guid or the current id
            return new SqlParameter()
            {
                ParameterName = "@Id",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output
            };
        }

        public SqlParameter BuildParam(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value);
        }

        protected T NullCheck<T>(object checkValue)
        {
            T outValue;
            if (checkValue == DBNull.Value)
                outValue = default(T);
            else
                outValue = (T)checkValue;
            return outValue;
        }
    }

}
