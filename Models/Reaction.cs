using System.ComponentModel.DataAnnotations;

namespace Rare_Serverside_GeckosTeam.Models
{
    public class Reaction
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string ImageUrl { get; set; }
        public List<PostReaction> Posts { get; set; }
    }
}
