using Microsoft.AspNetCore.Mvc;
using funda_assignment.Services;
using funda_assignment.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace funda_assignment.Controllers
{
    public class AgentsController : Controller
    {
        private IAgentsService agentsService;

        public AgentsController(IAgentsService agentsService)
        {
            this.agentsService = agentsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("agents")]
        public IActionResult GetTopTenAgents()
        {
            List<Agent> agents = agentsService.ListAgents() ?? new List<Agent>();
            return View(agents);
        }

        [Route("agents/garden")]
        public IActionResult GetTopTenAgentsWithGarden()
        {
            List<Agent> agents = agentsService.ListAgentsWithGarden() ?? new List<Agent>();
            return View(agents);
        }
    }
}