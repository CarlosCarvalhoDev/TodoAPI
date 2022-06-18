namespace TodoCustomList.Models.Todo.TodoVM
{
    public class ListTodoUserResponseViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<TodoResponseViewModel> TodoList { get; set; }
    }

    public class TodoResponseViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
