using System.ComponentModel.DataAnnotations;

namespace TodoCustomList.Models.Todo.Dto
{
    public class UpdateTodoDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
