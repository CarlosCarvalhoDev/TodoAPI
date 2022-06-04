using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoCustomList.Models
{
    [Table("TASK")]
    public class TaskTodoModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public bool IsCompleted { get; set; } = false;
        public Guid TodoId { get; set; }
        
        [ForeignKey("TodoId")]
        public TodoModel Todo { get; set; }

    }
}
