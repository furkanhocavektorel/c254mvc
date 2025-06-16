using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.entity
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public byte? Age { get; set; }
        public ERole Role { get; set; }
    }
}
