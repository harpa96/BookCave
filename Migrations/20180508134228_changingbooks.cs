using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookCave.Migrations
{
    public partial class changingbooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Books",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Books",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
