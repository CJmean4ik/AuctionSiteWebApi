using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionSite.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removeFullImageProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullImage",
                table: "LotConcrete");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullImage",
                table: "LotConcrete",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
