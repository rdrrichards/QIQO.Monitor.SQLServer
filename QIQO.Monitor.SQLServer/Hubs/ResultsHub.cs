using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace QIQO.Monitor.SQLServer
{
    public class ResultsHub : Hub
    {
        public async Task SendResult(string user, string message)
        {
            await Clients.All.SendAsync("Result", user, message);
        }
    }
}
