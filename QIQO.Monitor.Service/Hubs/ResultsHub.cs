using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace QIQO.Monitor.Service
{
    public class ResultsHub : Hub
    {
        private readonly ILogger<ResultsHub> _logger;

        public ResultsHub(ILogger<ResultsHub> logger)
        {
            _logger = logger;
        }
        public async Task SendResult(int resultType, string result)
        {
            try
            {
                await Clients.All.SendAsync("result", resultType, result);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
        public async Task Join(string userName)
        {
            await Clients.All.SendAsync("joined", userName);
        }
    }
}
