using System;
using MongoDB.Bson.Serialization.Attributes;
using WebBackend.Models;


namespace WebBackend.Models;

public class Assessment
{
    public DateTime Date { get; set; }

    public AssessmentType Type { get; set; }

    public string? CustomDescription { get; set; }
}

