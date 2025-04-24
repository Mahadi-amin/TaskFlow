using Microsoft.AspNetCore.Mvc;
using System.Web;
using TaskFlow.Application.ServicesInterface;
using TaskFlow.Domain.Entities;
using TaskFlow.Web.Models;

namespace TaskFlow.Web.Controllers
{
    public class TaskItemController : Controller
    {
        private readonly ITaskItemService _taskItemService;

        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }
        public async Task<IActionResult> Index()
        {
            var model = new TaskListModel();

            var status = await _taskItemService.GetStatusListAsync();
            model.SetAllStatuses(status);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetTaskItemJsonData([FromBody] TaskListModel model)
        {
            var result = await _taskItemService.GetAllTasksAsync(
                model.PageIndex,
                model.PageSize,
                model.SearchParams,
                model.FormatSortExpression("DueDate", "Title", "Description", "Priority", "StatusId", "StatusName"));

            var data = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from records in result.data
                        select new string[]
                            {
                                HttpUtility.HtmlEncode(records.DueDate.ToString("dd-MMM-yyyy")),
                                HttpUtility.HtmlEncode(records.Title),
                                HttpUtility.HtmlEncode(records.Description),
                                HttpUtility.HtmlEncode(records.Priority),
                                HttpUtility.HtmlEncode(records.Status.StatusName.ToString()),
                                records.Id.ToString()
                            }
                        ).ToArray()
            };
            return Json(data);
        }

        public async Task<IActionResult> Create()
        {
            var model = new TaskCreateModel();
            var tasks = await _taskItemService.GetTaskListAsync();
            var statuses = await _taskItemService.GetStatusListAsync();

            model.SetAllTasks(tasks);
            model.SetAllStatuses(statuses);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var taskItem = new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = DateTime.SpecifyKind(model.DueDate, DateTimeKind.Utc),
                    StatusId = model.StatusId,
                    Priority = model.Priority
                };

                await _taskItemService.CreateNewTaskAsync(taskItem);

                if (model.DueDate != DateTime.MinValue && model.PrerequisiteIds != null)
                {
                    await _taskItemService
                        .CreateNewDependencyAsync(taskItem.Id, model.PrerequisiteIds);
                }

                return RedirectToAction("Index");
            }

            var tasks = await _taskItemService.GetTaskListAsync();
            var statuses = await _taskItemService.GetStatusListAsync();

            model.SetAllTasks(tasks);
            model.SetAllStatuses(statuses);

            return View(model);
        }

        public async Task<IActionResult> Edit(Guid Id)
        {
            var task = await _taskItemService.GetTaskAsync(Id);

            var model = new TaskEditModel()
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Priority = task.Priority,
                StatusId = task.StatusId,

            };
            var tasks = await _taskItemService.GetTaskListAsync();
            var statuses = await _taskItemService.GetStatusListAsync();

            model.SetAllStatuses(statuses);
            model.SetAllTasks(tasks);
            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskEditModel model)
        {
            if (ModelState.IsValid)
            {
                var task = await _taskItemService.GetTaskAsync(model.Id);

                task.Title = model.Title;
                task.Description = model.Description;
                task.DueDate = DateTime.SpecifyKind(model.DueDate, DateTimeKind.Utc);
                task.StatusId = model.StatusId;
                task.Priority = model.Priority;


                await _taskItemService.UpdateTaskAsync(task);

                if (model.DueDate != DateTime.MinValue && model.PrerequisiteIds != null)
                {
                    await _taskItemService
                        .UpdateDependencyAsync(model.Id, model.PrerequisiteIds);
                }

                return RedirectToAction("Index");
            }

            var tasks = await _taskItemService.GetTaskListAsync();
            var statuses = await _taskItemService.GetStatusListAsync();

            model.SetAllTasks(tasks);
            model.SetAllStatuses(statuses);

            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _taskItemService.DeleteTaskAsync(id);
            return RedirectToAction("Index");
        }
    }
}
