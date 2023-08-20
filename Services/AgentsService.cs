using funda_assignment.Models;
using Newtonsoft.Json;

namespace funda_assignment.Services
{
	public class AgentsService : IAgentsService
	{
        private readonly ILogger<AgentsService> _logger;
        private static readonly HttpClient _httpClient = new ()
        {
            BaseAddress = new Uri("http://partnerapi.funda.nl/feeds/Aanbod.svc/json/ac1b0b1572524640a0ecc54de453ea9f/"),
        };

        public AgentsService(
            ILogger<AgentsService> logger
        )
		{
            _logger = logger;
        }

        public async Task FetchAgents()
        {
            using HttpResponseMessage response = await _httpClient.GetAsync("?type=koop&zo=/amsterdam/");
            HttpResponseMessage responseMessage = response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<AgentResponseModel>(responseString);

            _logger.LogInformation($"Executed AgentsService - FetchAgents {responseJson.Objects.Count}");
        }
    }

    public interface IAgentsService
    {
        Task FetchAgents();
    }
}

