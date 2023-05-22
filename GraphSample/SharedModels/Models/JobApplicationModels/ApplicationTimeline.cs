using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SharedModels.Models;

public class ApplicationTimeline
{

    //[BsonElement("TimelineID")]
    public int timelineID { get; set; }

    //[BsonElement("Assessments")]
    public List<Assessment> assessments { get; set; }

    //[BsonElement("Company")]
    public string company { get; set; }

    //[BsonElement("Role")]
    public string role { get; set; }

    //[BsonElement("AssociatedEmailAddresses")]
    public List<string> associatedEmailAddresses { get; set; }

    public int readEmailCount { get; set; }

    public bool hasUnreadEmails { get; set; }

    public Dictionary<string, bool> readEmails {get; set;}

}

