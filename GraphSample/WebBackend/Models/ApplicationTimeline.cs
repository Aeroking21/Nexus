using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebBackend.Models;

public class ApplicationTimeline
{

    [BsonId]
    public ObjectId Id { get; set; }

    public int TimelineID { get; set; }

    public List<Assessment> Assessments { get; set; }

    public string Company { get; set; }

    public string Role { get; set; }

	public List<string> AssociatedEmailAddresses { get; set; }

}

