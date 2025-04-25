namespace TaskFlow.Web.Models
{
    public class TaskBoardViewModel
    {
        public List<TaskEditModel> PendingTasks { get; set; }
        public List<TaskEditModel> InProgressTasks { get; set; }
        public List<TaskEditModel> CompletedTasks { get; set; }
    }

}
