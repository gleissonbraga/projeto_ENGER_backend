using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ENGER.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CLIENTES",
                columns: table => new
                {
                    CD_CLIENTE = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NM_RAZAO = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NM_FANTASIA = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CPF_CNPJ = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    NR_IERG = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    EMAIL = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LOGRADOURO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NUMERO = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CIDADE = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    BAIRRO = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CEP = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    UF = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    NR_TELEFONE = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    NR_CELULAR = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    DT_ENTRADA = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DT_ATUALIZACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CD_EMPRESA = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTES", x => x.CD_CLIENTE);
                    table.ForeignKey(
                        name: "FK_CLIENTES_EMPRESAS_CD_EMPRESA",
                        column: x => x.CD_EMPRESA,
                        principalTable: "EMPRESAS",
                        principalColumn: "CD_EMPRESA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTES_CD_EMPRESA",
                table: "CLIENTES",
                column: "CD_EMPRESA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENTES");
        }
    }
}
