using System.ComponentModel.DataAnnotations;

namespace Rare_Serverside_GeckosTeam.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public List<Post> Posts { get; set; }

    }
}
