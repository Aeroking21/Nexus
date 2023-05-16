using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core.Configuration;
using WebBackend.Models;
using static MongoDB.Driver.WriteConcern;
using System.Net.Mail;

namespace WebBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobApplicantsController : ControllerBase
{

    private readonly ILogger<JobApplicantsController> _logger;

    private readonly string _connectionString;

    private readonly MongoClient _mongoClient;

    private readonly IMongoCollection<JobApplicant> _collection;

    public JobApplicantsController(ILogger<JobApplicantsController> logger)
    {
        _logger = logger;
        _connectionString = "mongodb+srv://konSougiou:Pplkk3614@graphbackend.53zov5b.mongodb.net/?retryWrites=true&w=majority";
        _mongoClient = new MongoClient(_connectionString);
        _collection = _mongoClient.GetDatabase("GraphApplication").GetCollection<JobApplicant>("JobApplicationTimelines");

    }


    [HttpGet]
    [Route("get-timelines/{username}")]
    public async Task<IActionResult> GetApplicant(string username)
    {
        var filter = Builders<JobApplicant>.Filter.Eq("Username", username);
        var applicant = await _collection.Find(filter).FirstOrDefaultAsync();
        if (applicant != null)
        {
            return Ok(applicant.ApplicationTimelines);
        }
        else
        {
            return NotFound();
        }
    }


    [Route("create-applicant")]
    [HttpPost(Name = "PostApplicant")]
    public async Task<IActionResult> CreateApplicant([FromBody] JobApplicant jobApplicant)
    {
        try
        {
            await _collection.InsertOneAsync(jobApplicant);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [Route("add-email/{username}")]
    [HttpPost(Name = "addEmail")]
    public async Task<IActionResult> AddEmail([FromRoute] string username, [FromBody] Email email)
    {
        var filter = Builders<JobApplicant>.Filter.And(
            Builders<JobApplicant>.Filter.Eq("Username", username),
            Builders<JobApplicant>.Filter.Eq("ApplicationTimelines.TimelineID", email.TimelineID)
        );

        var update = Builders<JobApplicant>.Update.Push("ApplicationTimelines.$.AssociatedEmailAddresses", email.EmailAddress);

        try
        {
            var result = await _collection.UpdateOneAsync(filter, update);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [Route("remove-email/{username}")]
    [HttpPost(Name = "removeEmail")]
    public async Task<IActionResult> RemoveEmail([FromRoute] string username, [FromBody] Email email)
    {
        var userFilter = Builders<JobApplicant>.Filter.Eq(j => j.Username, username);

        var timelineFilter = Builders<ApplicationTimeline>.Filter.Eq(at => at.TimelineID, email.TimelineID);

        //var update = Builders<JobApplicant>.Update.PullFilter("ApplicationTimelines.$.AssociatedEmailAddresses", Builders<string>.Filter.Where(e => e == email.EmailAddress));
        List<string> associatedEmails = new();
        var applicant = _collection.Find(userFilter).ToList();
        var timelines = applicant[0].ApplicationTimelines;
        foreach (ApplicationTimeline timeline in timelines)
        {
            if (timeline.TimelineID == email.TimelineID)
            {
                associatedEmails = timeline.AssociatedEmailAddresses;
                break;
            }
        }
        try
        {

            if (associatedEmails.Count != 0)
            {
                associatedEmails.RemoveAll(e => e == email.EmailAddress);
                var filter = Builders<JobApplicant>.Filter.And(
                Builders<JobApplicant>.Filter.Eq("Username", username),
                Builders<JobApplicant>.Filter.Eq("ApplicationTimelines.TimelineID", email.TimelineID)
            );

                var update = Builders<JobApplicant>.Update.Set("ApplicationTimelines.$.AssociatedEmailAddresses", associatedEmails);

                var result = await _collection.UpdateOneAsync(filter, update);
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}




