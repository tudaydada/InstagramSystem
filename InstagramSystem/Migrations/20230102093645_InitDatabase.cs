using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InstagramSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblUserFeed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUserFeed", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblUserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Sex = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblUser_TblUserRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TblUserRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblPost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    hagtag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: true),
                    Privacy = table.Column<int>(type: "int", nullable: false),
                    FileURL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblPost_TblUser_UserId",
                        column: x => x.UserId,
                        principalTable: "TblUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblUserFollower",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserFollowerId = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUserFollower", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblUserFollower_TblUser_UserId",
                        column: x => x.UserId,
                        principalTable: "TblUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblPostComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPostComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblPostComment_TblPost_PostId",
                        column: x => x.PostId,
                        principalTable: "TblPost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblPostLike",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblPostLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblPostLike_TblPost_PostId",
                        column: x => x.PostId,
                        principalTable: "TblPost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TblUserRole",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblPost_UserId",
                table: "TblPost",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TblPostComment_PostId",
                table: "TblPostComment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_TblPostLike_PostId",
                table: "TblPostLike",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUser_RoleId",
                table: "TblUser",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserFollower_UserId",
                table: "TblUserFollower",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblPostComment");

            migrationBuilder.DropTable(
                name: "TblPostLike");

            migrationBuilder.DropTable(
                name: "TblUserFeed");

            migrationBuilder.DropTable(
                name: "TblUserFollower");

            migrationBuilder.DropTable(
                name: "TblPost");

            migrationBuilder.DropTable(
                name: "TblUser");

            migrationBuilder.DropTable(
                name: "TblUserRole");
        }
    }
}
