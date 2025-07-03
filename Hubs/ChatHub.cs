using ChatAppApi.Models;
using ChatAppApi.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppApi.Hubs;

public class ChatHub : Hub
{
    private readonly ChatService _chatService;

    public ChatHub(ChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task SendMessage(string user, string message)
    {
        var msg = new Message { User = user, Text = message, Timestamp = DateTime.UtcNow };
        await _chatService.CreateMessageAsync(msg);
        var msgToSend = new
        {
            user = msg.User,
            text = msg.Text,
            timestamp = msg.Timestamp.ToString("o")
        };
        await Clients.All.SendAsync("ReceiveMessage", msgToSend);
    }
}