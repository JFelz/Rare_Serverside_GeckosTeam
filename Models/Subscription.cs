using System.ComponentModel.DataAnnotations;

namespace Rare_Serverside_GeckosTeam.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime EndedOn { get; set;}
    }
}
