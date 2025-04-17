using Application.Contracts.Service.OutSide;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SignalRHub
{
    public class SignalRHub : Hub
    {
    }

    public class SignalRService : ISignalRService
    {
        private readonly IHubContext<SignalRHub> _hubContext;

        public SignalRService(IHubContext<SignalRHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendAsync(string method, object? arg)
        {
            await _hubContext.Clients.All.SendAsync(method, arg);
        }
    }
}
