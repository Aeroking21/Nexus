using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebBackend.Models;

public class JobApplicant
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Username { get; set; }

    public List<ApplicationTimeline> ApplicationTimelines { get; set; }

    public int TimelineCounter { get; set; }

}

