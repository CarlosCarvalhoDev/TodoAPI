using System.ComponentModel.DataAnnotations;

namespace TodoCustomList.Models.User.Dto
{
    public class CreateUserDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
