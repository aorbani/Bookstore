using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore.Migrations
{
    /// <inheritdoc />
    public partial class AddedAuthorBookRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "tblBook",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tblBook_AuthorId",
                table: "tblBook",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblBook_tblAuthors_AuthorId",
                table: "tblBook",
                column: "AuthorId",
                principalTable: "tblAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBook_tblAuthors_AuthorId",
                table: "tblBook");

            migrationBuilder.DropIndex(
                name: "IX_tblBook_AuthorId",
                table: "tblBook");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "tblBook");
        }
    }
}
