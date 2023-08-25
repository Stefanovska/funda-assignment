using funda_assignment.Models;

namespace funda_assignment.Services
{
    public interface IAgentsService
    {
        Task<List<Agent>> FetchAgents(string requestUri);
        List<Agent> ListAgents();
        List<Agent> ListAgentsWithGarden();
        Task SaveAgentsWithMostProperties();
        Task SaveAgentsWihMostPropertiesWithGarden();
    }
}

