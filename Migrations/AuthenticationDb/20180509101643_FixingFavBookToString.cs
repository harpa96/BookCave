using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookCave.Migrations.AuthenticationDb
{
    public partial class FixingFavBookToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoriteBookId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "FavoriteBook",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoriteBook",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteBookId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
