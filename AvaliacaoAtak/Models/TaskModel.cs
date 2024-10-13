using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

public enum TaskStatus
{
    Pending,     // 0
    InProgress,  // 1
    Done         // 2
}

public class TaskModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public TaskStatus Status { get; set; } 

    public DateTime CreatedAt { get; set; }
}
