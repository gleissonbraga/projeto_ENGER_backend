using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ENGER.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TablesBudget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ORCAMENTO",
                columns: table => new
                {
                    CD_ORCAMENTO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DS_ORCAMENTO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    STATUS_ORCAMENTO = table.Column<int>(type: "integer", nullable: false),
                    CD_EMPRESA = table.Column<int>(type: "integer", nullable: true),
                    CD_CLIENTE = table.Column<int>(type: "integer", nullable: true),
                    CD_USUARIO = table.Column<int>(type: "integer", nullable: true),
                    VL_TOTAL_ETAPAS = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    VL_TOTAL_MATERIAIS = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    VL_TOTAL = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    DS_OBSERVACAO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DT_ENTRADA = table.Column<DateTime>(type: "date", nullable: true),
                    DT_ATUALIZACAO = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORCAMENTO", x => x.CD_ORCAMENTO);
                    table.ForeignKey(
                        name: "FK_ORCAMENTO_CLIENTES_CD_CLIENTE",
                        column: x => x.CD_CLIENTE,
                        principalTable: "CLIENTES",
                        principalColumn: "CD_CLIENTE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ORCAMENTO_EMPRESAS_CD_EMPRESA",
                        column: x => x.CD_EMPRESA,
                        principalTable: "EMPRESAS",
                        principalColumn: "CD_EMPRESA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ORCAMENTO_ETAPAS",
                columns: table => new
                {
                    CD_ETAPA = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CD_ORCAMENTO = table.Column<int>(type: "integer", nullable: false),
                    DS_ETAPA = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NR_ORDEM = table.Column<int>(type: "integer", nullable: false),
                    STATUS_ORCAMENTO = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORCAMENTO_ETAPAS", x => x.CD_ETAPA);
                    table.ForeignKey(
                        name: "FK_ORCAMENTO_ETAPAS_ORCAMENTO_CD_ORCAMENTO",
                        column: x => x.CD_ORCAMENTO,
                        principalTable: "ORCAMENTO",
                        principalColumn: "CD_ORCAMENTO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ORCAMENTO_MAO_DE_OBRA",
                columns: table => new
                {
                    CD_ORCAMENTO_MO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StageId = table.Column<int>(type: "integer", nullable: false),
                    CD_CARGO = table.Column<int>(type: "integer", nullable: false),
                    QTD_HORAS_PREVISTAS = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    VL_HORA_CUSTO = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    PR_ENCARGOS_SOCIAIS = table.Column<decimal>(type: "numeric(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORCAMENTO_MAO_DE_OBRA", x => x.CD_ORCAMENTO_MO);
                    table.ForeignKey(
                        name: "FK_ORCAMENTO_MAO_DE_OBRA_ORCAMENTO_ETAPAS_StageId",
                        column: x => x.StageId,
                        principalTable: "ORCAMENTO_ETAPAS",
                        principalColumn: "CD_ETAPA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ORCAMENTO_MATERIAIS",
                columns: table => new
                {
                    CD_ORCAMENTO_MATERIAL = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StageId = table.Column<int>(type: "integer", nullable: false),
                    DS_MATERIAL = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CD_UNIDADE_MEDIDA = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    QTD_PREVISTA = table.Column<decimal>(type: "numeric(12,3)", nullable: false),
                    VL_UNITARIO_CUSTO = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    IN_FORNECIDO_CLIENTE = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORCAMENTO_MATERIAIS", x => x.CD_ORCAMENTO_MATERIAL);
                    table.ForeignKey(
                        name: "FK_ORCAMENTO_MATERIAIS_ORCAMENTO_ETAPAS_StageId",
                        column: x => x.StageId,
                        principalTable: "ORCAMENTO_ETAPAS",
                        principalColumn: "CD_ETAPA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ORCAMENTO_CD_CLIENTE",
                table: "ORCAMENTO",
                column: "CD_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_ORCAMENTO_CD_EMPRESA",
                table: "ORCAMENTO",
                column: "CD_EMPRESA");

            migrationBuilder.CreateIndex(
                name: "IX_ORCAMENTO_ETAPAS_CD_ORCAMENTO",
                table: "ORCAMENTO_ETAPAS",
                column: "CD_ORCAMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_ORCAMENTO_MAO_DE_OBRA_StageId",
                table: "ORCAMENTO_MAO_DE_OBRA",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_ORCAMENTO_MATERIAIS_StageId",
                table: "ORCAMENTO_MATERIAIS",
                column: "StageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ORCAMENTO_MAO_DE_OBRA");

            migrationBuilder.DropTable(
                name: "ORCAMENTO_MATERIAIS");

            migrationBuilder.DropTable(
                name: "ORCAMENTO_ETAPAS");

            migrationBuilder.DropTable(
                name: "ORCAMENTO");
        }
    }
}
