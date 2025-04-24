using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.ServicesInterface;
using TaskFlow.Web.Models;

namespace TaskFlow.Web.Controllers
{
    //[Authorize(Roles = "Admin, User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskItemService _taskItemService;

        public HomeController(ILogger<HomeController> logger, ITaskItemService taskItemService)
        {
            _logger = logger;
            _taskItemService = taskItemService;
        }

        public async Task<IActionResult> Index()
        {
            var upcomingTasksCount = await _taskItemService.GetUpcomingDueTaskCountAsync();
            var pendingTaskCount = await _taskItemService.GetAllPendingTaskAsync();
            var inProgressTaskCount = await _taskItemService.GetAllInProgressTaskAsync();
            var completedTaskCount = await _taskItemService.GetAllCompletedTaskAsync();

            ViewBag.UpcomingTasksCount = upcomingTasksCount;
            ViewBag.PendingTaskCount = pendingTaskCount;
            ViewBag.InProgressTaskCount = inProgressTaskCount;
            ViewBag.CompletedTaskCount = completedTaskCount;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
