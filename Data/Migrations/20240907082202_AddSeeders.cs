using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSeeders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "AdditionalTags", "ArtistName", "Description", "GenreId", "ImgUrl", "IsArchived", "IsPublic", "Title", "TrackUrl", "UploadDate", "UserId" },
                values: new object[,]
                {
                    { 1, null, null, null, 1, "www.google.com", false, true, "True Track", "www.google.com", new DateTime(2024, 9, 7, 10, 22, 0, 729, DateTimeKind.Local).AddTicks(3538), null },
                    { 2, null, null, null, 3, "www.google.com", false, true, "True Track 2", "www.google.com", new DateTime(2024, 9, 7, 10, 22, 0, 729, DateTimeKind.Local).AddTicks(3587), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tracks",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
