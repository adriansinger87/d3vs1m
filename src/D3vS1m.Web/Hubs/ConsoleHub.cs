using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D3vS1m.WebAPI.Hubs
{
    public class ConsoleHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"Hello World: {message}.");
        }
    }
}
