using System.ComponentModel.DataAnnotations;

namespace TodoCustomList.Models.TaskTodo.TaskTodoDTO
{
    public class CreateTaskTodoDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsCompleted { get; set; } = false;

        [Required]
        public Guid TodoId { get; set; }

    }
}
