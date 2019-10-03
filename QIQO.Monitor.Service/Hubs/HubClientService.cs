using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<HubClientService> _logger;
        private readonly IConfiguration _configuration;

        private readonly HubConnection connection;
        public HubClientService(ILogger<HubClientService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            //_logger.LogInformation($"HubClientService ctor");
            //_logger.LogInformation($"{_configuration["WS:BaseUrl"]}results");
            connection = new HubConnectionBuilder()
                .WithUrl($"{_configuration["WS:BaseUrl"]}results")
                .Build();
            // _logger.LogInformation($"HubConnectionState: >>> {connection.State}");
        }
        public async Task SendResult(ResultType resultType, object payload)
        {
            if (connection.State == HubConnectionState.Disconnected)
                await ConnectAsync();

            var result = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            // _logger.LogInformation($"SendResult result: {result}");
            try
            {
                // _logger.LogInformation($"SendResult HubConnectionState: >>> {connection.State}");
                if (connection.State == HubConnectionState.Connected)
                {
                    // _logger.LogInformation($"HubConnectionState Connected >> {resultType.ToString()}");
                    await connection.InvokeAsync("SendResult", resultType, result);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
        private async Task ConnectAsync()
        {
            await connection.StartAsync();
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
