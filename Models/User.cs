using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;

namespace Rare_Serverside_GeckosTeam.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
        public DateTime Created { get; set; }
        public bool Active { get; set; }
        public bool IsStaff { get; set; }
        public string Uid { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Post> Posts { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public List<PostReaction> PostReactions { get; set; }
    }
}
