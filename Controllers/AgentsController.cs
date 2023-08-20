using Microsoft.AspNetCore.Mvc;
using funda_assignment.Services;

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
            //agentsService.ListAgents();
            return View();
        }
    }
}

