using ChatAppApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatAppApi.Services;

public class ChatService
{
    private readonly IMongoCollection<Message> _messages;

    public ChatService(IOptions<MongoDBSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _messages = database.GetCollection<Message>(settings.Value.CollectionName);
    }

    public async Task<List<Message>> GetMessagesAsync() =>
        await _messages.Find(_ => true).ToListAsync();

    public async Task CreateMessageAsync(Message message) =>
        await _messages.InsertOneAsync(message);
}