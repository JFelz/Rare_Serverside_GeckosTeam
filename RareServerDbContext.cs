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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Post>().HasData(new Post[]
            {
                new Post {Id = 1, UserId = 1, CategoryId = 1, Title = "Post 1", ImageUrl = "www.image1.com", Content = "post 1 content", IsApproved = true },
                new Post {Id = 2, UserId = 2, CategoryId = 2, Title = "Post 2", ImageUrl = "www.image2.com", Content = "post 2 content", IsApproved = true },
                new Post {Id = 3, UserId = 3, CategoryId = 3, Title = "Post 3", ImageUrl = "www.image3.com", Content = "post 3 content", IsApproved = true },
            });

            modelBuilder.Entity<Comment>().HasData(new Comment[]
            {
                new Comment { Id = 1, UserId = 1, PostId = 1, Content = "This is some smaple data 1", CreatedOn = new DateTime(2023, 1, 3)},
                new Comment { Id = 2, UserId = 2, PostId = 2, Content = "This is some smaple data 2", CreatedOn = new DateTime(2023, 1, 4)},
                new Comment { Id = 3, UserId = 3, PostId = 3, Content = "This is some smaple data 3", CreatedOn = new DateTime(2023, 1, 5)},
            });

            modelBuilder.Entity<PostReaction>().HasData(new PostReaction[]
            {
                new PostReaction {Id = 1, UserId = 1, PostId = 1, ReactionId = 1 },
                new PostReaction {Id = 2, UserId = 2, PostId = 2, ReactionId = 2 },
                new PostReaction {Id = 3, UserId = 3, PostId = 3, ReactionId = 3 },
            });

            modelBuilder.Entity<Tag>().HasData(new Tag[]
            {
                new Tag { Id = 1, Label = "Tag1" },
                new Tag { Id = 2, Label = "Tag2" },
                new Tag { Id = 3, Label = "Tag3" },
            });

            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category { Id = 1, Label = "News" },
                new Category { Id = 2, Label = "Sports" },
                new Category { Id = 3, Label = "Entertainment" }
            });

            modelBuilder.Entity<Subscription>().HasData(new Subscription[]
           {
                new Subscription { Id = 1, FollowerId = 1, UserId = 1, CreatedOn = DateTime.Now.AddDays(-10), EndedOn = DateTime.Now },
                new Subscription { Id = 2, FollowerId = 2, UserId = 2, CreatedOn = DateTime.Now.AddMonths(-3), EndedOn = DateTime.Now },
                new Subscription { Id = 3, FollowerId = 3, UserId = 3, CreatedOn = DateTime.Now.AddYears(-1), EndedOn = DateTime.Now },
           });

            modelBuilder.Entity<Reaction>().HasData(new Reaction[]
           {
                new Reaction { Id = 1, Label = "This is Label 1", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSaqb4DXftSl3QrnGdU-3YGnRzoaCG3OeM1yg&usqp=CAU"},
                new Reaction { Id = 2, Label = "This is Label 2", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ2Q92VIuShEeHGLkcD77lixte1A-ahFgGm_w&usqp=CAU"},
                new Reaction { Id = 3, Label = "This is Label 3", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS3RiKVD7XZmoDVuzkd0m7_ugGlgXGqrTtkiQ&usqp=CAU"},
           });

            modelBuilder.Entity<User>().HasData(new User[]
            {
                new User { Id = 1, FirstName = "user", LastName = "one", Bio = "bio1", Email = "user1@user.com", ProfileImage = "image1", Created =  new DateTime(2023, 01, 01), Active = true, IsStaff = true, Uid = "user1"},
                new User { Id = 2, FirstName = "user", LastName = "two", Bio = "bio2", Email = "user2@user.com", ProfileImage = "image2", Created =  new DateTime(2023, 02, 02), Active = true, IsStaff = true, Uid = "user2"},
                new User { Id = 3, FirstName = "user", LastName = "three", Bio = "bio3", Email = "user3@user.com", ProfileImage = "image3", Created =  new DateTime(2023, 03, 03), Active = true, IsStaff = true, Uid = "user3"},
            });

        }
    }
}
