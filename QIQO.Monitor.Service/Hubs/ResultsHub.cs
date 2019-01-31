using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace QIQO.Monitor.Service
{
    public class ResultsHub : Hub
    {
        public async Task SendResult(string resultType, string result)
        {
            await Clients.All.SendAsync("result", resultType, result);
        }
        public async Task Join(string userName)
        {
            await Clients.All.SendAsync("joined", userName);
        }
    }
}
