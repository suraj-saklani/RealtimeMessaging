using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealtimeMessaging.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addingchatKeyinchattable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatKey",
                table: "Chats",
                type: "nvarchar(910)",
                maxLength: 910,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ChatKey",
                table: "Chats",
                column: "ChatKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Chats_ChatKey",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ChatKey",
                table: "Chats");
        }
    }
}
