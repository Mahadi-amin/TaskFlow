using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.ServicesInterface;
using TaskFlow.Domain.Entities;
using TaskFlow.Web.Dtos;
using TaskFlow.Web.Models;

namespace TaskFlow.Web.Controllers
{
    [Authorize(Roles = "Admin, User")]
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
            var tasks = await _taskItemService.GetTaskByStatusAsync();

            var upcomingTasksCount = await _taskItemService.GetUpcomingDueTaskCountAsync();
            var pendingTaskCount = await _taskItemService.GetAllTaskCountByStatusAsync(TaskStatusNames.Pending);
            var inProgressTaskCount = await _taskItemService.GetAllTaskCountByStatusAsync(TaskStatusNames.InProgress);
            var completedTaskCount = await _taskItemService.GetAllTaskCountByStatusAsync(TaskStatusNames.Completed);

            ViewBag.UpcomingTasksCount = upcomingTasksCount;
            ViewBag.PendingTaskCount = pendingTaskCount;
            ViewBag.InProgressTaskCount = inProgressTaskCount;
            ViewBag.CompletedTaskCount = completedTaskCount;

            return View(tasks);
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
