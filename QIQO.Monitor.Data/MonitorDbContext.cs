using QIQO.Monitor.Core;
using QIQO.Monitor.Core.Contracts;

namespace QIQO.Monitor.Data
{
    public interface IMonitorDbContext : IDbContext { }
    public class MonitorDbContext : DbContextBase, IMonitorDbContext //, IDisposable
    {
        // public SQLServerDbContext() : this(null, null) { }
        public MonitorDbContext(string connectionString) : base(connectionString)
        {
            // Log.LogInformation("Hello from the AccountDbContext!");
        }

        //public override int ExecuteProcedureNonQuery(string procedureName, IEnumerable<SqlParameter> parameters)
        //{
        //    var cmd = new SqlCommand(procedureName, _connection) { CommandType = CommandType.StoredProcedure };
        //    int ret_val;

        //    foreach (var sparam in parameters)
        //        cmd.Parameters.Add(BuildParameter(sparam));

        //    try
        //    {
        //        _connection.Open();
        //        ret_val = cmd.ExecuteNonQuery();
        //        _connection.Close();
        //        if (cmd.Parameters["@key"] != null)
        //        {
        //            int key = (int)cmd.Parameters["@key"].Value;
        //            if (key > ret_val)
        //                return key;
        //        }
        //        return ret_val;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogError(ex.Message);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        _connection.Close();
        //    }

        //}
    }
}
