using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "BorrowedAt",
                table: "BorrowRecords");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PublishedAt",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Members",
                newName: "MembershipDate");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Members",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "ReturnedAt",
                table: "BorrowRecords",
                newName: "ReturnDate");

            migrationBuilder.RenameColumn(
                name: "DueAt",
                table: "BorrowRecords",
                newName: "BorrowDate");

            migrationBuilder.RenameColumn(
                name: "CopiesAvailable",
                table: "Books",
                newName: "TotalCopies");

            migrationBuilder.AddColumn<int>(
                name: "AvailableCopies",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableCopies",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "MembershipDate",
                table: "Members",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Members",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "BorrowRecords",
                newName: "ReturnedAt");

            migrationBuilder.RenameColumn(
                name: "BorrowDate",
                table: "BorrowRecords",
                newName: "DueAt");

            migrationBuilder.RenameColumn(
                name: "TotalCopies",
                table: "Books",
                newName: "CopiesAvailable");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Members",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Members",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Members",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "BorrowedAt",
                table: "BorrowRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Books",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedAt",
                table: "Books",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
