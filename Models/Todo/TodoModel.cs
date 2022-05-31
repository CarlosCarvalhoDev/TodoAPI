using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoCustomList.Models
{
    [Table("TODO")]
    public class TodoModel
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public UserModel User { get; set; }
    }
}
