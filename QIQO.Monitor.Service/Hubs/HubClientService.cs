using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace QIQO.Monitor.Service
{
    public interface IHubClientService
    {
        Task SendResult(ResultType resultType, object payload);
    }
    public class HubClientService : IHubClientService
    {
        private readonly HubConnection connection;
        public HubClientService(IConfiguration configuration)
        {
            connection = new HubConnectionBuilder()
                .WithUrl(configuration["WS:BaseUrl"])
                .Build();
            connection.StartAsync();
        }
        public async Task SendResult(ResultType resultType, object payload)
        {
            await connection.InvokeAsync("result", resultType, payload);
        }
    }
    public enum ResultType
    {
        Blocking,
        OpenTransaction
    }
}
