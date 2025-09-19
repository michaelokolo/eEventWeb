using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBuilder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Freelancers_FreelancerId1",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Organizers_OrganizerId1",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_OrganizerId1",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Applications_FreelancerId1",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "OrganizerId1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FreelancerId1",
                table: "Applications");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizerId",
                table: "Events",
                type: "nvarchar(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FreelancerId",
                table: "Applications",
                type: "nvarchar(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Organizers_IdentityGuid",
                table: "Organizers",
                column: "IdentityGuid");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Freelancers_IdentityGuid",
                table: "Freelancers",
                column: "IdentityGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_FreelancerId",
                table: "Applications",
                column: "FreelancerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Freelancers_FreelancerId",
                table: "Applications",
                column: "FreelancerId",
                principalTable: "Freelancers",
                principalColumn: "IdentityGuid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Organizers_OrganizerId",
                table: "Events",
                column: "OrganizerId",
                principalTable: "Organizers",
                principalColumn: "IdentityGuid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Freelancers_FreelancerId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Organizers_OrganizerId",
                table: "Events");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Organizers_IdentityGuid",
                table: "Organizers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Freelancers_IdentityGuid",
                table: "Freelancers");

            migrationBuilder.DropIndex(
                name: "IX_Events_OrganizerId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Applications_FreelancerId",
                table: "Applications");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizerId",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)");

            migrationBuilder.AddColumn<int>(
                name: "OrganizerId1",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FreelancerId",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)");

            migrationBuilder.AddColumn<int>(
                name: "FreelancerId1",
                table: "Applications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId1",
                table: "Events",
                column: "OrganizerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_FreelancerId1",
                table: "Applications",
                column: "FreelancerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Freelancers_FreelancerId1",
                table: "Applications",
                column: "FreelancerId1",
                principalTable: "Freelancers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Organizers_OrganizerId1",
                table: "Events",
                column: "OrganizerId1",
                principalTable: "Organizers",
                principalColumn: "Id");
        }
    }
}
