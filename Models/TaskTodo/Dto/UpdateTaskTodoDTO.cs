using System.ComponentModel.DataAnnotations;

namespace TodoCustomList.Models.TaskTodo.TaskTodoDTO
{
    public class UpdateTaskTodoDTO
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public Guid TodoId { get; set; }
    }
}
