using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Rare_Serverside_GeckosTeam;
using Rare_Serverside_GeckosTeam.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7284",
                                              "http://localhost:3000")
                                               .AllowAnyHeader()
                                               .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<RareServerDbContext>(builder.Configuration["RareServerDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

// Tag Endpoints

// Create New Tag
app.MapPost("/CreateNewTag", async (RareServerDbContext db, Tag tag) =>
{
    db.Tags.Add(tag);
    await db.SaveChangesAsync();
    return Results.Ok(tag);
});

// Get All Tags
app.MapGet("/GetAllTags", (RareServerDbContext db) =>
{
    var tags = db.Tags.OrderBy(tag => tag.Label).ToList();
    return Results.Ok(tags);
});

// Edit Tag
app.MapPut("/EditTag", async (RareServerDbContext db, int id, Tag updatedTag) =>
{
    var existingTag = await db.Tags.FindAsync(id);
    if (existingTag == null)
        return Results.NotFound();

    existingTag.Label = updatedTag.Label;
    await db.SaveChangesAsync();

    return Results.Ok(existingTag);
});

// Delete Tag
app.MapDelete("/DeleteTag", async (RareServerDbContext db, int id) =>
{
    var tag = await db.Tags.FindAsync(id);
    if (tag == null)
        return Results.NotFound();

    db.Tags.Remove(tag);
    await db.SaveChangesAsync();

    return Results.Ok(tag);
});

// Add Tag To Post
app.MapPost("/AddTagToPost", (RareServerDbContext db, int PostId, int TagId, int UsersId) =>
{
    //Get Post
    Post SelectedPost = db.Posts.FirstOrDefault(x => x.Id == PostId);
    Tag SelectedTag = db.Tags.FirstOrDefault(t => t.Id == TagId);
    User SelectedUser = db.Users.FirstOrDefault(u => u.Id == UsersId);

    PostTag NewPostTag = new PostTag()
    {
        PostId = SelectedPost.Id,
        TagId = SelectedTag.Id,
        UserId = SelectedUser.Id,
    };

    db.PostTags.Add(NewPostTag);
    db.SaveChanges();
    return Results.NoContent();
});

// Remove Tag From Post
app.MapDelete("/RemoveTagFromPost", (RareServerDbContext db, int PostId, int TagId) =>
{
    // Get the PostTag entry for the specified PostId and TagId
    var postTag = db.PostTags.FirstOrDefault(pt => pt.PostId == PostId && pt.TagId == TagId);

    if (postTag == null)
        return Results.NotFound();

    // Remove the PostTag entry
    db.PostTags.Remove(postTag);
    db.SaveChanges();

    return Results.NoContent();
});

// POST ENDPOINTS

app.MapGet("/api/posts", (RareServerDbContext db) =>
{
    return db.Posts.ToList();
});

app.MapDelete("/api/posts/{id}", (RareServerDbContext db, int id) =>
{
    Post postToDelete = db.Posts.SingleOrDefault(post => post.Id == id);
    if (postToDelete == null)
    {
        return Results.NotFound();
    }
    db.Posts.Remove(postToDelete);
    db.SaveChanges();
    return Results.Ok(db.Posts);
});

app.MapGet("/api/posts/{id}", (RareServerDbContext db, int id) =>
{
    var post = db.Posts
                 .Include(p => p.User)
                 .Include(p => p.Category)
                 .Include(p => p.Comments)
                 .Include(p => p.Reactions)
                 .Include(p => p.Tags)
                 .Single(p => p.Id == id);

    //var tags = db.Tags
    //             .Where(pt => pt.PostId == id)
    //             .Select(pt => pt.Tag)
    //             .ToList();

    //var postData = new
    //{
    //    Post = post,
    //    Tags = tags
    //};

    return Results.Ok(post);
});

app.MapGet("/api/userposts/{id}", (RareServerDbContext db, int Userid) =>
{
    return db.Posts.Include(p => p.User).
                    Include(p => p.Category).
                    Include(p => p.Comments).
                    Include(p => p.Reactions).
                    Include(p => p.Tags).
                    Where(p => p.UserId == Userid);
});

app.MapPut("/api/posts/{id}", (RareServerDbContext db, int id, Post post) =>
{
    Post postToUpdate = db.Posts.SingleOrDefault(p => p.Id == id);
    if (postToUpdate == null)
    {
        return Results.NotFound();
    }
    postToUpdate.CategoryId = post.CategoryId;
    postToUpdate.Title = post.Title;
    postToUpdate.ImageUrl = post.ImageUrl;
    postToUpdate.Content = post.Content;
    db.SaveChanges();
    return Results.Ok(postToUpdate);
});

app.MapPost("/api/posts", (RareServerDbContext db, Post post) =>
{
    Post newPost = new()
    {
        Content = post.Content,
        Title = post.Title,
        ImageUrl = post.ImageUrl,
        IsApproved = post.IsApproved,
        PublicationDate = post.PublicationDate,
    };

    newPost.User = db.Users.FirstOrDefault(user => user.Id == post.UserId);
    newPost.Category = db.Categories.FirstOrDefault(cat => cat.Id == post.CategoryId);
    if (post.Tags?.Count > 0)
    {
        newPost.Tags = new();
        foreach (var postTags in post.Tags)
        {
            Tag newTag = db.Tags.FirstOrDefault(tag => tag.Id == postTags.Id);
            newPost.Tags.Add(newTag);
        }
    }
    db.Posts.Add(newPost);
    db.SaveChanges();
    return Results.Created($"/api/posts/{post.Id}", post);
});

// Comment Endpoints

// get comments
app.MapGet("/Comments", (RareServerDbContext db) =>
{
    return db.Comments.ToList();
});
//Get comment by ID
app.MapGet("/api/CommentsbyID/{id}", (RareServerDbContext db, int id) =>
{
    var comment = db.Comments.Where(s => s.Id == id)
    .Include(x => x.User)
    .Include(s => s.Post).ToList();
    return comment;
}
);
// Add a comment
app.MapPost("api/Comment", async (RareServerDbContext db, Comment comment) =>
{
    db.Comments.Add(comment);
    db.SaveChanges();
    return Results.Created($"/api/songs{comment.Id}", comment);
});
//update Comment
app.MapPut("api/Comments/{id}", async (RareServerDbContext db, int id, Comment comment) =>
{
    Comment commentToUpdate = await db.Comments.SingleOrDefaultAsync(comment => comment.Id == id);
    if (commentToUpdate == null)
    {
        return Results.NotFound();
    }
    commentToUpdate.Id = comment.Id;
    commentToUpdate.UserId = comment.UserId;
    commentToUpdate.PostId = comment.PostId;
    commentToUpdate.Content = comment.Content;
    db.SaveChanges();
    return Results.NoContent();
});
// delete Comment
app.MapDelete("api/Comment/{id}", (RareServerDbContext db, int id) =>
{
    Comment comment = db.Comments.SingleOrDefault(comment => comment.Id == id);
    if (comment == null)
    {
        return Results.NotFound();
    }
    db.Comments.Remove(comment);
    db.SaveChanges();
    return Results.NoContent();
});

// CATEGORIES ENDPOINTS

// Get Categories

app.MapGet("/rareserver/categories", (RareServerDbContext db) =>
{
    List<Category> categories = db.Categories.ToList();
    if (categories.Count == 0)
    {
        return Results.NotFound();
    }

    return Results.Ok(categories);
});

// Get Category By Id

app.MapGet("/rareserver/categories/{id}", (RareServerDbContext db, int id) =>
{
    Category category = db.Categories.SingleOrDefault(c => c.Id == id);
    if (category == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(category);
});

// Post Category

app.MapPost("/rareserver/categories", (RareServerDbContext db, Category category) =>
{
    try
    {
        db.Add(category);
        db.SaveChanges();
        return Results.Created($"/rareserver/categories/{category.Id}", category);
    }
    catch (DbUpdateException)
    {
        return Results.NotFound();
    }
});

// Update Category

app.MapPut("/rareserver/categories/{categoryId}", (RareServerDbContext db, int categoryId, Category category) =>
{
    Category updateCategory = db.Categories.SingleOrDefault(c => c.Id == categoryId);
    if (updateCategory == null)
    {
        return Results.NotFound();
    }
    updateCategory.Label = category.Label;
    db.SaveChanges();
    return Results.Ok(updateCategory);
});

// Delete Category

app.MapDelete("/rareserver/categories/{categoryId}", (RareServerDbContext db, int categoryId) =>
{
    Category deleteCategory = db.Categories.FirstOrDefault(c => c.Id == categoryId);
    if (deleteCategory == null)
    {
        return Results.NotFound();
    }
    db.Remove(deleteCategory);
    db.SaveChanges();
    return Results.Ok(deleteCategory);
});


// SUBSCRIBE TO NEW USER

app.MapPost("/rareserver/subscribe", (RareServerDbContext db, int followerId, int userId) =>
{
    // Find the follower and the author
    var follower = db.Users.FirstOrDefault(u => u.Id == followerId);
    var userToSubscribeTo = db.Users.FirstOrDefault(u => u.Id == userId);

    if (follower == null || userToSubscribeTo == null)
    {
        return Results.NotFound("Invalid user to subscribe to.");
    }

    // Create a new subscription
    var subscription = new Subscription
    {
        FollowerId = followerId,
        UserId = userId,
        CreatedOn = DateTime.Now
    };

    db.Subscriptions.Add(subscription);
    db.SaveChanges();

    return Results.Created($"/rareserver/subscribe/{subscription.Id}", subscription);
});


// UNSUBSCRIBE TO USER BY ID

app.MapDelete("/rareserver/unsubscribe", (RareServerDbContext db, int followerId, int userId) =>
{
    // Find the follower and user
    var follower = db.Users.FirstOrDefault(f => f.Id == followerId);
    var userToUnsubscribeFrom = db.Users.FirstOrDefault(u => u.Id == userId);

    if (follower == null || userToUnsubscribeFrom == null)
    {
        return Results.NotFound("Invalid user to unsubscribe to.");
    }

    // Find the existing subscription
    var existingSubscription = db.Subscriptions.FirstOrDefault(s => s.FollowerId == followerId && s.UserId == userId);

    if (existingSubscription == null)
    {
        return Results.NotFound("Subscription not found.");
    }

    // Update the end DateTime to the current time
    existingSubscription.EndedOn = DateTime.Now;
    db.SaveChanges();

    return Results.Ok();
});

// User EndPoints

// Get users
app.MapGet("/users", (RareServerDbContext db) =>
{
    return db.Users.ToList();
});

// View Single User

app.MapGet("/users/{Id}", (RareServerDbContext db, int Id) =>
{
    return db.Users.FirstOrDefault(x => x.Id == Id);
});

// Update User
app.MapPut("/users/{UserId}", (RareServerDbContext db, int UserId, User NewUser) =>
{
    User SelectedUser = db.Users.FirstOrDefault(x => x.Id == UserId);
    if (SelectedUser == null)
    {
        return Results.NotFound("This user is not found in the database. Please Try again!");
    }

    SelectedUser.FirstName = NewUser.FirstName;
    SelectedUser.LastName = NewUser.LastName;
    SelectedUser.Bio = NewUser.Bio;
    SelectedUser.Email = NewUser.Email;
    SelectedUser.ProfileImage = NewUser.ProfileImage;
    SelectedUser.IsStaff = NewUser.IsStaff;
    db.SaveChanges();
    return Results.NoContent();

});

// View Users post
app.MapGet("/users/{ID}/posts", (RareServerDbContext db, int ID) =>
{

    return db.Users.Where(x => x.Id == ID)
                   .Include(p => p.Posts)
                   .ToList();
});

//Create New User - Challenge for Auth. Leave for Last.

app.MapGet("/checkuser/{uid}", (RareServerDbContext db, string uid) =>
{
    var user = db.Users.Where(x => x.Uid == uid).ToList();
    if (uid == null)
    {
        return Results.NotFound();
    }
    else
    {
        return Results.Ok(user);
    }
});

// Reaction Endpoints

//Get Reactions
app.MapGet("/reactions", (RareServerDbContext db) =>
{
    return db.Reactions.ToList();
});

//Get Single Reaction
app.MapGet("/reactions/{Id}", (RareServerDbContext db, int Id) =>
{
    return db.Reactions.FirstOrDefault(x => x.Id == Id);
});
// Add Reaction to Post
/**
 In the front end, an icon emoji will be under every post. Upon click, it will post that reaction Id to the Post's reaction table.
 **/
app.MapPost("/post/postreaction", (RareServerDbContext db, int PostId, int ReactId, int UsersId) =>
{
    //Get Post
    Post SelectedPost = db.Posts.FirstOrDefault(x => x.Id == PostId);
    Reaction SelectedReaction = db.Reactions.FirstOrDefault(r => r.Id == ReactId);
    User SelectedUser = db.Users.FirstOrDefault(u => u.Id == UsersId);

    PostReaction NewPostReact = new PostReaction()
    {
        PostId = SelectedPost.Id,
        ReactionId = SelectedReaction.Id,
        UserId = SelectedUser.Id,
    };

    db.PostsReaction.Add(NewPostReact);
    db.SaveChanges();
    return Results.NoContent();
});

//Remove Reaction
//Check it by user so it wont delete other peoples same reaction.
app.MapDelete("/post", (RareServerDbContext db, int Id) =>
{
    PostReaction DeletedReaction = db.PostsReaction.FirstOrDefault(x => x.Id == Id);

    db.PostsReaction.Remove(DeletedReaction);
    db.SaveChanges();
    return Results.Ok();

});

app.Run();
