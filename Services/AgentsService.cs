using System.Text.RegularExpressions;
using funda_assignment.Models;
using Newtonsoft.Json;

namespace funda_assignment.Services
{
	public class AgentsService : IAgentsService
	{
        private readonly IConfiguration _configuration;
        private readonly ILogger<AgentsService> _logger;
        private readonly HttpClient _httpClient;

        public AgentsService(
            ILogger<AgentsService> logger,
            IConfiguration configuration
        )
		{
            _logger = logger;
            _configuration = configuration;
            _httpClient = new()
            {
                BaseAddress = new Uri($"{_configuration["FUNDA_BASE_API_URL"]}/feeds/Aanbod.svc/json/{_configuration["FUNDA_API_KEY"]}/"),
            };
        }

        public async Task FetchAgents()
        {
            try
            {
                int page = 1;
                int requestsSent = 0;
                bool fetchingInProgress = true;

                List<PropertyInfoResponseModel> allProperties = new List<PropertyInfoResponseModel>();
                List<Agent> allAgents = new List<Agent>();

                DateTime start = DateTime.Now;

                while (fetchingInProgress)
                {
                    if (requestsSent < 100 && DateTime.Now < start.AddMinutes(1))
                    {
                        using HttpResponseMessage response = await _httpClient.GetAsync($"?type=koop&zo=/amsterdam/&page={page}&pagesize=25");
                        HttpResponseMessage responseMessage = response.EnsureSuccessStatusCode();
                        requestsSent++;

                        var responseString = await response.Content.ReadAsStringAsync();
                        var responseJson = JsonConvert.DeserializeObject<AgentResponseModel>(responseString);
                        _logger.LogInformation($"FetchAgents - Page: {responseJson.Paging.HuidigePagina} - Count: {responseJson.Objects.Count} - Total pages: {responseJson.Paging.AantalPaginas}");

                        if (responseJson.Paging.HuidigePagina == responseJson.Paging.AantalPaginas)
                        {
                            fetchingInProgress = false;
                            break;
                        }
                        else
                        {
                            allProperties = allProperties.Concat(responseJson.Objects).ToList();
                            page++;
                        }
                    }

                    if (DateTime.Now > start.AddMinutes(1))
                    {
                        start = DateTime.Now;
                        requestsSent = 0;
                    }
                }

                var propertiesGroupedByAgentId = allProperties
                    .GroupBy(p => new { p.MakelaarId, p.MakelaarNaam })
                    .Select(g => new
                    {
                        Id = g.Key.MakelaarId,
                        Name = g.Key.MakelaarNaam,
                        NumberOfProperties = g.Count()
                    })
                    .OrderByDescending(a => a.NumberOfProperties)
                    .Take(10);
            } catch(Exception ex)
            {
                _logger.LogInformation($"Error {ex.Message}");
            }
            
        }
    }

    public interface IAgentsService
    {
        Task FetchAgents();
    }
}

