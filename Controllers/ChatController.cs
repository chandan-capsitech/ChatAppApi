using ChatAppApi.Models;
using ChatAppApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly ChatService _chatService;

    public ChatController(ChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet("messages")]
    public async Task<ApiResponse<List<Message>>> GetMessages()
    {
        var res = new ApiResponse<List<Message>>();
        try
        {
            var messages = await _chatService.GetMessagesAsync();
            res.Status = true;
            res.Message = "Fetched...";
            res.Result = messages;
        }
        catch (Exception ex) {
            res.Status = false;
            res.Message = ex.Message;
        }
        return res;
    }

    [HttpPost("messages")]
    public async Task<ApiResponse<Message>> PostMessage([FromBody] Message message)
    {
        var res = new ApiResponse<Message>();
        try
        {
            if (message == null || string.IsNullOrEmpty(message.User) || string.IsNullOrEmpty(message.Text))
            {
                res.Status = false;
                res.Message = "Invalid message data.";
                return res;
            }

            message.Timestamp = DateTime.UtcNow;
            await _chatService.CreateMessageAsync(message);

            res.Status = true;
            res.Message = "Message created successfully.";
            res.Result = message;
        }
        catch (Exception ex)
        {
            res.Status = false;
            res.Message = ex.Message;
        }

        return res;
    }
}