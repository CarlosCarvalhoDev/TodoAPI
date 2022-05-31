using System.ComponentModel.DataAnnotations;

namespace TodoCustomList.Models.Todo.Dto
{
    public class UpdateTodoDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
