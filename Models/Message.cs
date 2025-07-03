using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatAppApi.Models;

public class Message
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string User { get; set; } = string.Empty;
    public string Text { get; set; } = String.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

public class ApiResponse<T>
{
    public bool Status { get; set; } = false;
    public string Message { get; set; } = string.Empty ;

    public T Result { get; set; }
}