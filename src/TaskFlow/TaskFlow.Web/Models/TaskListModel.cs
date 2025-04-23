using Microsoft.AspNetCore.Mvc.Rendering;
using TaskFlow.Domain.Dtos;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain;
using TaskFlow.Infrastructure.Utilities;

namespace TaskFlow.Web.Models
{
    public class TaskListModel : DataTables
    {
        public TaskItemDto? SearchParams { get; set; }
        public IList<SelectListItem> StatusList { get; private set; }
        public IList<SelectListItem> PriorityList { get; private set; }

        public void SetAllStatuses(IList<Status> statuItems)
        {
            StatusList = statuItems.ToSelectList(x => x.StatusName, y => y.Id.ToString());
        }
    }

}
