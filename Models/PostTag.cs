namespace Rare_Serverside_GeckosTeam.Models
{
    public class PostTag
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
        public Tag Tag { get; set; }
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}
