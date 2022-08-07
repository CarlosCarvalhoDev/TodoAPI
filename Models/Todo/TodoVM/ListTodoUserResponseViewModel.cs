namespace TodoCustomList.Models.Todo.TodoVM
{
    public class TodosUsersResponseViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<TodoResponseViewModel> TodoList { get; set; }
    }

    public class TodoResponseViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
