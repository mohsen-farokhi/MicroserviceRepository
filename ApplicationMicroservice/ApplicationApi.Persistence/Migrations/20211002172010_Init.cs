using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationApi.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessTypes",
                columns: table => new
                {
                    Value = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessTypes", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationGroups_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AccessTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_AccessTypes_AccessTypeId",
                        column: x => x.AccessTypeId,
                        principalTable: "AccessTypes",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperationsOfGroups",
                columns: table => new
                {
                    OperationGroupId = table.Column<int>(type: "int", nullable: false),
                    OperationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationsOfGroups", x => new { x.OperationGroupId, x.OperationId });
                    table.ForeignKey(
                        name: "FK_OperationsOfGroups_OperationGroups_OperationGroupId",
                        column: x => x.OperationGroupId,
                        principalTable: "OperationGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OperationsOfGroups_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AccessTypes",
                columns: new[] { "Value", "Name" },
                values: new object[] { 0, "عمومی" });

            migrationBuilder.InsertData(
                table: "AccessTypes",
                columns: new[] { "Value", "Name" },
                values: new object[] { 1, "ثبت نام شده" });

            migrationBuilder.InsertData(
                table: "AccessTypes",
                columns: new[] { "Value", "Name" },
                values: new object[] { 2, "خصوصی" });

            migrationBuilder.CreateIndex(
                name: "IX_OperationGroups_ApplicationId",
                table: "OperationGroups",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_AccessTypeId",
                table: "Operations",
                column: "AccessTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_ApplicationId",
                table: "Operations",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsOfGroups_OperationId",
                table: "OperationsOfGroups",
                column: "OperationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationsOfGroups");

            migrationBuilder.DropTable(
                name: "OperationGroups");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "AccessTypes");

            migrationBuilder.DropTable(
                name: "Applications");
        }
    }
}
