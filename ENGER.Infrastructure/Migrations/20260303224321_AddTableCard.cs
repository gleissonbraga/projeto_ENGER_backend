using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ENGER.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DT_INICIO",
                table: "ASSINATURA",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_PROXIMA_COBRANCA",
                table: "ASSINATURA",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "CARTOES",
                columns: table => new
                {
                    CD_CARTAO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CD_MP_CUSTOMER = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CD_MP_CARTAO = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NR_FINAL_CARTAO = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    NM_BANDEIRA = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    NR_MES_EXPIRACAO = table.Column<int>(type: "integer", nullable: false),
                    NR_ANO_EXPIRACAO = table.Column<int>(type: "integer", nullable: false),
                    CD_EMPRESA = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARTOES", x => x.CD_CARTAO);
                    table.ForeignKey(
                        name: "FK_CARTOES_EMPRESAS_CD_EMPRESA",
                        column: x => x.CD_EMPRESA,
                        principalTable: "EMPRESAS",
                        principalColumn: "CD_EMPRESA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARTOES_CD_EMPRESA",
                table: "CARTOES",
                column: "CD_EMPRESA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CARTOES");

            migrationBuilder.DropColumn(
                name: "DT_INICIO",
                table: "ASSINATURA");

            migrationBuilder.DropColumn(
                name: "DT_PROXIMA_COBRANCA",
                table: "ASSINATURA");
        }
    }
}
