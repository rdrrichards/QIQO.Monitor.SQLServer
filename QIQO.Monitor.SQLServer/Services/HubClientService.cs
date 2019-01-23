using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace QIQO.Monitor.SQLServer
{
    public interface IHubClientService
    {
        Task SendResult(ResultType resultType, object payload);
    }
    public class HubClientService : IHubClientService
    {
        private readonly HubConnection connection;
        public HubClientService()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("/results")
                .Build();
            connection.StartAsync();
        }
        public async Task SendResult(ResultType resultType, object payload)
        {
            await connection.InvokeAsync("SendResult", resultType, payload);
        }
    }
    public enum ResultType
    {
        Version
    }
}
