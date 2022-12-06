using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto_Final.Migrations
{
    public partial class AddCarrinho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarrinhoId",
                table: "Produto",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Consumidor",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Carrinho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConsumidorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carrinho_Consumidor_ConsumidorId",
                        column: x => x.ConsumidorId,
                        principalTable: "Consumidor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CarrinhoId",
                table: "Produto",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Carrinho_ConsumidorId",
                table: "Carrinho",
                column: "ConsumidorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Carrinho_CarrinhoId",
                table: "Produto",
                column: "CarrinhoId",
                principalTable: "Carrinho",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Carrinho_CarrinhoId",
                table: "Produto");

            migrationBuilder.DropTable(
                name: "Carrinho");

            migrationBuilder.DropIndex(
                name: "IX_Produto_CarrinhoId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "CarrinhoId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Consumidor");
        }
    }
}
