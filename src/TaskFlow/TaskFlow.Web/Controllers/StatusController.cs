using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Services;

namespace TaskFlow.Web.Controllers
{
    public class StatusController : Controller
    {
        private readonly ILogger<StatusController> _logger;
        private readonly IStatusService _statusService;

        public StatusController(ILogger<StatusController> logger,
            IStatusService statusService)
        {
            _logger = logger;
            _statusService = statusService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var status = _statusService.GetAllStatus();
            return View();
        }


    }
}
