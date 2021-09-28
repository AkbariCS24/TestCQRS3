using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCQRS3.Infrastructure.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Field1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item2s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Field1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Field2 = table.Column<bool>(type: "bit", nullable: false),
                    Field3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item2s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item2s_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item2s_ItemId",
                table: "Item2s",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item2s");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
