using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EfCoreExample.Migrations
{
    public partial class AddFeaturedPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FeaturedPostId",
                table: "Blogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_FeaturedPostId",
                table: "Blogs",
                column: "FeaturedPostId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Posts_FeaturedPostId",
                table: "Blogs",
                column: "FeaturedPostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Posts_FeaturedPostId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_FeaturedPostId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "FeaturedPostId",
                table: "Blogs");
        }
    }
}
