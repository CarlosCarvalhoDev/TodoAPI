namespace TodoCustomList.Models.TaskTodo.TaskTodoVM
{
    public class UpdateTaskTodoResponseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public Guid TodoId { get; set; }
        public string TodoName { get; set; }
    }
}
