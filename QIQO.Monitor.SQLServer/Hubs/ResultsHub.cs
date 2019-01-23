﻿using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace QIQO.Monitor.SQLServer
{
    public class ResultsHub : Hub
    {
        public async Task SendResult(string resultType, string result)
        {
            await Clients.All.SendAsync("result", resultType, result);
        }
    }
}
