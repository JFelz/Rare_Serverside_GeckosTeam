namespace Rare_Serverside_GeckosTeam.Models
{
    public class PostReaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
        public Reaction Reaction { get; set; }
        public int PostId { get; set; }
        public int ReactionId { get; set; }

    }
}
