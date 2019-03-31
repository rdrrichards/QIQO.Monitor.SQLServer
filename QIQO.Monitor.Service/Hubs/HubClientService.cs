using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
                .WithUrl($"{configuration["WS:BaseUrl"]}results")
                .Build();
            connection.StartAsync();
        }
        public async Task SendResult(ResultType resultType, object payload)
        {
            var result = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            await connection.InvokeAsync("SendResult", resultType, result);
        }
    }
    public enum ResultType
    {
        Health,
        Blocking,
        OpenTransaction,
        WaitStats
    }
}
