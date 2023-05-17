using System;
using System.Text.Json;
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
	}
}

