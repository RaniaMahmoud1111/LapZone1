using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapZone.Migrations
{
    /// <inheritdoc />
    public partial class custom2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LaptopDetail");

            migrationBuilder.DropTable(
                name: "Review");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LaptopDetail",
                columns: table => new
                {
                    LaptopID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CPU = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GPU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HasWebcam = table.Column<bool>(type: "bit", nullable: true),
                    RAM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ScreenSize = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    Storage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    _Weight = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LaptopDe__19F026A4D5C77AFB", x => x.LaptopID);
                    table.ForeignKey(
                        name: "FK_Product_LaptopDetail",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Review__74BC79AEB92B741D", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Product_Review",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK__User_Review",
                        column: x => x.UserID,
                        principalTable: "_User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__LaptopDe__B40CC6ECC4FF7DEB",
                table: "LaptopDetail",
                column: "ProductID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_ProductID",
                table: "Review",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserID",
                table: "Review",
                column: "UserID");
        }
    }
}
