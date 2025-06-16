using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.entity
{
    [Table("Posts")]
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }

        [ForeignKey("User")]
        public long User__Id { get; set; }

        public User User { get; set; } // Navigation property


    }
}
