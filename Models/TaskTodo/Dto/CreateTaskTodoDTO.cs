using System.ComponentModel.DataAnnotations;

namespace TodoCustomList.Models.TaskTodo.TaskTodoDTO
{
    public class CreateTaskTodoDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid TodoId { get; set; }

    }
}
