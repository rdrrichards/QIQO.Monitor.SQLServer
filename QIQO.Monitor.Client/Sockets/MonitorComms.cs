using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace QIQO.Monitor.Client
{
    public class MonitorComms : Hub
    {
        private readonly ILogger<MonitorComms> _logger;

        public MonitorComms(ILogger<MonitorComms> logger)
        {
            _logger = logger;
        }
        public async Task Send(string payload)
        {
            try
            {
                _logger.LogInformation($"Sockets results message sent to all");
                await Clients.All.SendAsync("monitorresult", payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
        public override Task OnConnectedAsync()
        {
            var user = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(user))
            {
                _logger.LogInformation($"Sockets connect: {user}");
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var user = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(user))
            {
                var auditMsg = "no ";
                if (exception != null)
                {
                    auditMsg = string.Empty;
                    _logger.LogError(exception.ToString());
                }
                _logger.LogInformation($"Sockets disconnect: {user}; {auditMsg}exception");
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}
