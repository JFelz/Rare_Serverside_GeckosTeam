using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Rare_Serverside_GeckosTeam.Migrations
{
    /// <inheritdoc />
    public partial class PostTagsTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostTags_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTags_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "EndedOn" },
                values: new object[] { new DateTime(2023, 9, 6, 12, 31, 39, 709, DateTimeKind.Local).AddTicks(6677), new DateTime(2023, 9, 16, 12, 31, 39, 709, DateTimeKind.Local).AddTicks(6704) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "EndedOn" },
                values: new object[] { new DateTime(2023, 6, 16, 12, 31, 39, 709, DateTimeKind.Local).AddTicks(6707), new DateTime(2023, 9, 16, 12, 31, 39, 709, DateTimeKind.Local).AddTicks(6710) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "EndedOn" },
                values: new object[] { new DateTime(2022, 9, 16, 12, 31, 39, 709, DateTimeKind.Local).AddTicks(6712), new DateTime(2023, 9, 16, 12, 31, 39, 709, DateTimeKind.Local).AddTicks(6714) });

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_PostId",
                table: "PostTags",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TagId",
                table: "PostTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_UserId",
                table: "PostTags",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "EndedOn" },
                values: new object[] { new DateTime(2023, 9, 4, 18, 22, 56, 408, DateTimeKind.Local).AddTicks(2505), new DateTime(2023, 9, 14, 18, 22, 56, 408, DateTimeKind.Local).AddTicks(2534) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "EndedOn" },
                values: new object[] { new DateTime(2023, 6, 14, 18, 22, 56, 408, DateTimeKind.Local).AddTicks(2536), new DateTime(2023, 9, 14, 18, 22, 56, 408, DateTimeKind.Local).AddTicks(2537) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "EndedOn" },
                values: new object[] { new DateTime(2022, 9, 14, 18, 22, 56, 408, DateTimeKind.Local).AddTicks(2539), new DateTime(2023, 9, 14, 18, 22, 56, 408, DateTimeKind.Local).AddTicks(2540) });
        }
    }
}
