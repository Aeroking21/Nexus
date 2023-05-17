using System;
using SharedModels.Models;

namespace GraphSample.Services
{
	public interface IBackendApiService
	{
        public Task<List<ApplicationTimeline>> getUserTimelinesAsync(string username);

    }
}

