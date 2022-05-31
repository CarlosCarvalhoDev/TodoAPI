using System.ComponentModel.DataAnnotations;

namespace TodoCustomList.Models.Todo.Dto
{
    public class CreateTodoDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
