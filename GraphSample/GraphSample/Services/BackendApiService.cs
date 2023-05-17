using System;
using System.Text.Json;
using System.Net.Http.Json;
using System.Linq;
using SharedModels.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace GraphSample.Services
{
	public class BackendApiService: IBackendApiService
	{

        private readonly HttpClient httpClient;

        public BackendApiService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
        }

		public async Task<List<ApplicationTimeline>> getUserTimelinesAsync(string username)
		{
			var response = await httpClient.GetAsync($"https://localhost:7023/api/JobApplicants/get-timelines/{username}");
            string responseBody = await response.Content.ReadAsStringAsync();

            // Deserialize the response body into your custom model
            List<ApplicationTimeline> timelines = BsonSerializer.Deserialize<List<ApplicationTimeline>>(responseBody);
			return timelines;
        }

        public async Task<ApplicationTimeline> getUserTimelineAsync(string username, int timelineID)
        {
            var response = await httpClient.GetAsync($"https://localhost:7023/api/JobApplicants/get-timeline/{username}/{timelineID}");
            string responseBody = await response.Content.ReadAsStringAsync();

            // Deserialize the response body into your custom model
            ApplicationTimeline timelines = BsonSerializer.Deserialize<ApplicationTimeline>(responseBody);
            return timelines;
        }


        public async Task<bool> removeEmail(string email, string username, int timelineID)
		{
			var selectedEmail = new EmailBson { emailAddress = email, timelineID = timelineID };

            var response = await httpClient.PostAsJsonAsync($"https://localhost:7023/api/JobApplicants/remove-email/{username}", selectedEmail);

            return response.IsSuccessStatusCode;

        }


	}
}

