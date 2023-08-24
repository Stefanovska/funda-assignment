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

        // GET: /agents/
        public IActionResult Index()
        {
            List<Agent> agents = agentsService.ListAgents();
            return View(agents);
        }
    }
}