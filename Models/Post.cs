using System.ComponentModel.DataAnnotations;
namespace Rare_Serverside_GeckosTeam.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Comment> Comments { get; set; }
        public List<PostReaction> Reactions { get; set; }
        public List<Tag> Tags { get; set; }
        public string? Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? Content { get; set; }
        public bool? IsApproved { get; set; }
    }
}