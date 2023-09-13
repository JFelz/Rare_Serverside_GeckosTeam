using System.ComponentModel.DataAnnotations;
namespace Rare_Serverside_GeckosTeam.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
