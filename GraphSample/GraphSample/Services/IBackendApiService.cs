using System;
using SharedModels.Models;

namespace GraphSample.Services
{
	public interface IBackendApiService
	{
        public Task<List<ApplicationTimeline>> getUserTimelinesAsync(string username);
        public Task<ApplicationTimeline> getUserTimelineAsync(string username, int timelineID);
        public Task<bool> removeEmail(string email, string username, int timelineID);
        public Task<bool> addEmail(string email, string username, int timelineID);
        public Task<bool> removeAssessment(Assessment assessment, string username, int timelineID);
        public Task<bool> addAssessment(Assessment assessment, string username, int timelineID);
        public Task<int> addTimeline(string username, TimelineBson newTimeLineBson);

    }
}

