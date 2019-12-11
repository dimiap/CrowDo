using Microsoft.EntityFrameworkCore.Migrations;

namespace CrowDo.Migrations
{
    public partial class crowdodb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Packages_PackagesId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_PackagesId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "PackagesId",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "IsDeleted",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVisits",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Packages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Packages_ProjectId",
                table: "Packages",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Projects_ProjectId",
                table: "Packages",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Projects_ProjectId",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Packages_ProjectId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "NumberOfVisits",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Packages");

            migrationBuilder.AddColumn<int>(
                name: "PackagesId",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PackagesId",
                table: "Projects",
                column: "PackagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Packages_PackagesId",
                table: "Projects",
                column: "PackagesId",
                principalTable: "Packages",
                principalColumn: "PackagesId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
