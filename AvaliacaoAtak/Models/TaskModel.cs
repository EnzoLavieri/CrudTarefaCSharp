using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class TaskModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }

}

public enum Status
{
    Pending = 0,
    InProgress = 1,
    Done = 2
    //tentar arrumar depos para aceitar apenas as palavras au inves dos numeros
    //da pra fazer timestamps e o Id automatico?
}