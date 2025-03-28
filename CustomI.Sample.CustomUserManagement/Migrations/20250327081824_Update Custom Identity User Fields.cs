using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomI.Sample.CustomUserManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomIdentityUserFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "CstUserMngt",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                schema: "CstUserMngt",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "CstUserMngt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Picture",
                schema: "CstUserMngt",
                table: "Users");
        }
    }
}
