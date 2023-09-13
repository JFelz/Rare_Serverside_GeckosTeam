using Microsoft.EntityFrameworkCore;
using Rare_Serverside_GeckosTeam.Models;

namespace Rare_Serverside_GeckosTeam
{
    public class RareServerDbContext :DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostReaction> PostsReaction { get; set; }

        public RareServerDbContext(DbContextOptions<RareServerDbContext> context) : base(context)
        {

        }
    }
}
