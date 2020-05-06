using Microsoft.AspNetCore.SignalR;

namespace WebChat.SignalRServer
{
    public class ChatHub : Hub
    {
        public void SendToAll(string name, string message)
        {
            Clients.All.InvokeAsync("sendToAll", name, message);
        }
    }
}