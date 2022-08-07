using TodoCustomList.Models.TaskTodo.TaskTodoVM;

namespace TodoCustomList.Models.Todo.TodoVM
{
    public class GetTodoResponseViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<TaskTodoResponseViewModel> Tasks { get; set; }

    }


}
