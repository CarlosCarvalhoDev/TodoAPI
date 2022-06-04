namespace TodoCustomList.Models.Todo.TodoVM
{
    public class TodoSumaryResponseViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
