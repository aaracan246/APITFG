using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StatsApi.Models;

public class Score
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Name { get; set; }
    public int Points { get; set; }
}
