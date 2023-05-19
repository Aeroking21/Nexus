using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Core.Configuration;
using SharedModels.Models;
using static MongoDB.Driver.WriteConcern;
using System.Net.Mail;
using System.Collections.Generic;

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
    public async Task<IActionResult> GetTimelines(string username)
    {
        var filter = Builders<JobApplicant>.Filter.Eq("username", username);
        var applicant = await _collection.Find(filter).FirstOrDefaultAsync();
        if (applicant != null)
        {
            return Ok(applicant.applicationTimelines);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet]
    [Route("get-timeline/{username}/{timelineID}")]
    public async Task<IActionResult> GetTimeline(string username, int timelineID)
    {
        var filter = Builders<JobApplicant>.Filter.And(
            Builders<JobApplicant>.Filter.Eq("username", username));

        var applicant = await _collection.Find(filter).FirstOrDefaultAsync();

        if (applicant != null)
        {
            foreach (ApplicationTimeline timeline in applicant.applicationTimelines)
            {
                if (timeline.timelineID == timelineID)
                {
                    return Ok(timeline);
                }
            }
            return NotFound();

        }
        else
        {
            return NotFound();
        }
    }



    [Route("create-applicant")]
    [HttpPost]
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

    [Route("add-timeline/{username}")]
    [HttpPost]
    public async Task<IActionResult> AddTimeline([FromRoute] string username, [FromBody] TimelineBson timelineBson)
    {
        var filter = Builders<JobApplicant>.Filter.And(
            Builders<JobApplicant>.Filter.Eq("username", username));

        var applicant = await _collection.Find(filter).FirstOrDefaultAsync();
        int newTimelineID = applicant.timelineCounter;
        applicant.timelineCounter++;
        ApplicationTimeline newTimeline = new ApplicationTimeline
        {
            company = timelineBson.company,
            role = timelineBson.role,
            assessments = new List<Assessment>(),
            associatedEmailAddresses = new List<string>(),
            timelineID = newTimelineID
        };

        var timelineUpdate = Builders<JobApplicant>.Update.Push("applicationTimelines", newTimeline);
        var counterUpdate = Builders<JobApplicant>.Update.Set("timelineCounter", (newTimelineID + 1));

        try
        {
            var result1 = await _collection.UpdateOneAsync(filter, timelineUpdate);
            var result2 = await _collection.UpdateOneAsync(filter, counterUpdate);
            return Ok(newTimelineID);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [Route("add-email/{username}")]
    [HttpPost]
    public async Task<IActionResult> AddEmail([FromRoute] string username, [FromBody] EmailBson emailBson)
    {
        var filter = Builders<JobApplicant>.Filter.And(
            Builders<JobApplicant>.Filter.Eq("username", username),
            Builders<JobApplicant>.Filter.Eq("applicationTimelines.timelineID", emailBson.timelineID)
        );

        var update = Builders<JobApplicant>.Update.Push("applicationTimelines.$.associatedEmailAddresses", emailBson.emailAddress);

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
    [HttpPost]
    public async Task<IActionResult> RemoveEmail([FromRoute] string username, [FromBody] EmailBson emailBson)
    {
        var userFilter = Builders<JobApplicant>.Filter.Eq(j => j.username, username);

        var timelineFilter = Builders<ApplicationTimeline>.Filter.Eq(at => at.timelineID, emailBson.timelineID);

        //var update = Builders<JobApplicant>.Update.PullFilter("ApplicationTimelines.$.AssociatedEmailAddresses", Builders<string>.Filter.Where(e => e == email.EmailAddress));
        List<string> associatedEmails = new();
        var applicant = _collection.Find(userFilter).ToList();
        var timelines = applicant[0].applicationTimelines;
        foreach (ApplicationTimeline timeline in timelines)
        {
            if (timeline.timelineID == emailBson.timelineID)
            {
                associatedEmails = timeline.associatedEmailAddresses;
                break;
            }
        }
        try
        {

            if (associatedEmails.Count != 0)
            {
                associatedEmails.RemoveAll(e => e == emailBson.emailAddress);
                var filter = Builders<JobApplicant>.Filter.And(
                Builders<JobApplicant>.Filter.Eq("username", username),
                Builders<JobApplicant>.Filter.Eq("applicationTimelines.timelineID", emailBson.timelineID)
            );

                var update = Builders<JobApplicant>.Update.Set("applicationTimelines.$.associatedEmailAddresses", associatedEmails);

                var result = await _collection.UpdateOneAsync(filter, update);
                return Ok();
            }

            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }




    [Route("add-assessment/{username}")]
    [HttpPost]
    public async Task<IActionResult> AddAssesment([FromRoute] string username, [FromBody] AssessmentBson assessmentBson)
    {
        var filter = Builders<JobApplicant>.Filter.And(
            Builders<JobApplicant>.Filter.Eq("username", username),
            Builders<JobApplicant>.Filter.Eq("applicationTimelines.timelineID", assessmentBson.timelineID)
        );

        var update = Builders<JobApplicant>.Update.Push("applicationTimelines.$.assessments", assessmentBson.assessment);

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




    [Route("update-assessment-status/{username}")]
    [HttpPost]
    public async Task<IActionResult> UpdateAssesmentStatus([FromRoute] string username, [FromBody] AssessmentBson assessmentBson)
    {
        var userFilter = Builders<JobApplicant>.Filter.Eq(j => j.username, username);

        var timelineFilter = Builders<ApplicationTimeline>.Filter.Eq(at => at.timelineID, assessmentBson.timelineID);

        //var update = Builders<JobApplicant>.Update.PullFilter("ApplicationTimelines.$.AssociatedEmailAddresses", Builders<string>.Filter.Where(e => e == email.EmailAddress));
        List<Assessment> assessments = new();
        var applicant = _collection.Find(userFilter).ToList();
        var timelines = applicant[0].applicationTimelines;
        foreach (ApplicationTimeline timeline in timelines)
        {
            if (timeline.timelineID == assessmentBson.timelineID)
            {
                assessments = timeline.assessments;
                break;
            }
        }
        try
        {

            if (assessments.Count != 0)
            {
                foreach (Assessment assessment in assessments.Where(a => a.date == assessmentBson.assessment.date))
                {
                    assessment.status = assessmentBson.assessment.status;
                };
                var filter = Builders<JobApplicant>.Filter.And(
                Builders<JobApplicant>.Filter.Eq("username", username),
                Builders<JobApplicant>.Filter.Eq("applicationTimelines.timelineID", assessmentBson.timelineID)
            );

                var update = Builders<JobApplicant>.Update.Set("applicationTimelines.$.assessments", assessments);

                var result = await _collection.UpdateOneAsync(filter, update);
                return Ok();
            }

            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [Route("remove-assessment/{username}")]
    [HttpPost]
    public async Task<IActionResult> RemoveAssessment([FromRoute] string username, [FromBody] AssessmentBson assessmentBson)
    {
        var userFilter = Builders<JobApplicant>.Filter.Eq(j => j.username, username);

        var timelineFilter = Builders<ApplicationTimeline>.Filter.Eq(at => at.timelineID, assessmentBson.timelineID);

        //var update = Builders<JobApplicant>.Update.PullFilter("ApplicationTimelines.$.AssociatedEmailAddresses", Builders<string>.Filter.Where(e => e == email.EmailAddress));
        List<Assessment> assessments = new();
        var applicant = _collection.Find(userFilter).ToList();
        var timelines = applicant[0].applicationTimelines;
        foreach (ApplicationTimeline timeline in timelines)
        {
            if (timeline.timelineID == assessmentBson.timelineID)
            {
                assessments = timeline.assessments;
                break;
            }
        }
        try
        {

            if (assessments.Count != 0)
            {
                assessments.RemoveAll(a => a.date == assessmentBson.assessment.date);
                var filter = Builders<JobApplicant>.Filter.And(
                Builders<JobApplicant>.Filter.Eq("username", username),
                Builders<JobApplicant>.Filter.Eq("applicationTimelines.timelineID", assessmentBson.timelineID)
            );

                var update = Builders<JobApplicant>.Update.Set("applicationTimelines.$.assessments", assessments);

                var result = await _collection.UpdateOneAsync(filter, update);
                return Ok();
            }

            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}




