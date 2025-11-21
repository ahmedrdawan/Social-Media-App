using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaAPI.Migrations
{
    /// <inheritdoc />
    public partial class createRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name" , "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
                    { "1", "User", "USER", Guid.NewGuid().ToString() },
                    { "2", "Admin" , "ADMIN", Guid.NewGuid().ToString() }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValues: new object[] { "1", "2" });
        }
    }
}
