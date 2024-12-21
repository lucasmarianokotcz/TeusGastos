using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeusGastos.Shared.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mercado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Endereco = table.Column<string>(type: "varchar(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mercado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeMedida",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Sigla = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeMedida", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotaCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MercadoId = table.Column<int>(type: "int", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "date", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotaCompra_Mercado_MercadoId",
                        column: x => x.MercadoId,
                        principalTable: "Mercado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoEAN = table.Column<string>(type: "char(13)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(255)", nullable: false),
                    UnidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_UnidadeMedida_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "UnidadeMedida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemNotaCompra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotaId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "[Quantidade] * ([ValorUnitario] - [Desconto])")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemNotaCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemNotaCompra_NotaCompra_NotaId",
                        column: x => x.NotaId,
                        principalTable: "NotaCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemNotaCompra_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemNotaCompra_NotaId",
                table: "ItemNotaCompra",
                column: "NotaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemNotaCompra_ProdutoId",
                table: "ItemNotaCompra",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_NotaCompra_MercadoId",
                table: "NotaCompra",
                column: "MercadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_UnidadeId",
                table: "Produto",
                column: "UnidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemNotaCompra");

            migrationBuilder.DropTable(
                name: "NotaCompra");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Mercado");

            migrationBuilder.DropTable(
                name: "UnidadeMedida");
        }
    }
}
