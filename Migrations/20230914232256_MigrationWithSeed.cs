using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Rare_Serverside_GeckosTeam.Migrations
{
    /// <inheritdoc />
    public partial class MigrationWithSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RareUserId",
                table: "Posts");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Label" },
                values: new object[,]
                {
                    { 1, "News" },
                    { 2, "Sports" },
                    { 3, "Entertainment" }
                });

            migrationBuilder.InsertData(
                table: "Reactions",
                columns: new[] { "Id", "ImageUrl", "Label" },
                values: new object[,]
                {
                    { 1, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSaqb4DXftSl3QrnGdU-3YGnRzoaCG3OeM1yg&usqp=CAU", "This is Label 1" },
                    { 2, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ2Q92VIuShEeHGLkcD77lixte1A-ahFgGm_w&usqp=CAU", "This is Label 2" },
                    { 3, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS3RiKVD7XZmoDVuzkd0m7_ugGlgXGqrTtkiQ&usqp=CAU", "This is Label 3" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Label" },
                values: new object[,]
                {
                    { 1, "Tag1" },
                    { 2, "Tag2" },
                    { 3, "Tag3" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "Bio", "Created", "Email", "FirstName", "IsStaff", "LastName", "ProfileImage", "Uid" },
                values: new object[,]
                {
                    { 1, true, "bio1", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user1@user.com", "user", true, "one", "image1", "user1" },
                    { 2, true, "bio2", new DateTime(2023, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "user2@user.com", "user", true, "two", "image2", "user2" },
                    { 3, true, "bio3", new DateTime(2023, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "user3@user.com", "user", true, "three", "image3", "user3" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "CategoryId", "Content", "ImageUrl", "IsApproved", "PublicationDate", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "post 1 content", "www.image1.com", true, null, "Post 1", 1 },
                    { 2, 2, "post 2 content", "www.image2.com", true, null, "Post 2", 2 },
                    { 3, 3, "post 3 content", "www.image3.com", true, null, "Post 3", 3 }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "CreatedOn", "EndedOn", "FollowerId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 9, 4, 18, 22, 56, 408, DateTimeKind.Local).AddTicks(2505), new DateTime(2023, 9, 14, 18, 22, 56, 408, DateTimeKind.Local).AddTicks(2534), 1, 1 },
                    { 2, new DateTime(2023, 6, 14, 18, 22, 56, 408, DateTimeKind.Local).AddTicks(2536), new DateTime(2023, 9, 14, 18, 22, 56, 408, DateTimeKind.Local).AddTicks(2537), 2, 2 },
                    { 3, new DateTime(2022, 9, 14, 18, 22, 56, 408, DateTimeKind.Local).AddTicks(2539), new DateTime(2023, 9, 14, 18, 22, 56, 408, DateTimeKind.Local).AddTicks(2540), 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedOn", "PostId", "UserId" },
                values: new object[,]
                {
                    { 1, "This is some smaple data 1", new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, "This is some smaple data 2", new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 3, "This is some smaple data 3", new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "PostsReaction",
                columns: new[] { "Id", "PostId", "ReactionId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 2, 2 },
                    { 3, 3, 3, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PostsReaction",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PostsReaction",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PostsReaction",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reactions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reactions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reactions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "RareUserId",
                table: "Posts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
