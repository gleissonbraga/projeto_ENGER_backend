using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ENGER.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ADMIN",
                table: "EMPRESAS",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BAIRRO",
                table: "EMPRESAS",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "EMPRESAS",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CIDADE",
                table: "EMPRESAS",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CPF_CNPJ",
                table: "EMPRESAS",
                type: "character varying(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_NASCIMENTO",
                table: "EMPRESAS",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EMAIL",
                table: "EMPRESAS",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LOGRADOURO",
                table: "EMPRESAS",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NM_FANTASIA",
                table: "EMPRESAS",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NM_RAZAO",
                table: "EMPRESAS",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NR_IERG",
                table: "EMPRESAS",
                type: "character varying(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NR_TELEFONE",
                table: "EMPRESAS",
                type: "character varying(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NUMERO",
                table: "EMPRESAS",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UF",
                table: "EMPRESAS",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TIPO_ASSINATURA",
                columns: table => new
                {
                    CD_TP_ASSINATURA = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DS_TP_ASSINATURA = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    VL_ASSINATURA = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_ASSINATURA", x => x.CD_TP_ASSINATURA);
                });

            migrationBuilder.CreateTable(
                name: "ASSINATURA",
                columns: table => new
                {
                    CD_ASSINATURA = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CD_CHAVE = table.Column<Guid>(type: "uuid", maxLength: 40, nullable: false),
                    DT_VENCIMENTO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    STATUS_ASSINATURA = table.Column<int>(type: "integer", nullable: false),
                    DT_PAGAMENTO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TypeSubscriptionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASSINATURA", x => x.CD_ASSINATURA);
                    table.UniqueConstraint("AK_ASSINATURA_CD_CHAVE", x => x.CD_CHAVE);
                    table.ForeignKey(
                        name: "FK_ASSINATURA_TIPO_ASSINATURA_TypeSubscriptionId",
                        column: x => x.TypeSubscriptionId,
                        principalTable: "TIPO_ASSINATURA",
                        principalColumn: "CD_TP_ASSINATURA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMPRESAS_CD_ASSINATURA",
                table: "EMPRESAS",
                column: "CD_ASSINATURA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ASSINATURA_TypeSubscriptionId",
                table: "ASSINATURA",
                column: "TypeSubscriptionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EMPRESAS_ASSINATURA_CD_ASSINATURA",
                table: "EMPRESAS",
                column: "CD_ASSINATURA",
                principalTable: "ASSINATURA",
                principalColumn: "CD_CHAVE",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EMPRESAS_ASSINATURA_CD_ASSINATURA",
                table: "EMPRESAS");

            migrationBuilder.DropTable(
                name: "ASSINATURA");

            migrationBuilder.DropTable(
                name: "TIPO_ASSINATURA");

            migrationBuilder.DropIndex(
                name: "IX_EMPRESAS_CD_ASSINATURA",
                table: "EMPRESAS");

            migrationBuilder.DropColumn(
                name: "ADMIN",
                table: "EMPRESAS");

            migrationBuilder.DropColumn(
                name: "BAIRRO",
                table: "EMPRESAS");

            migrationBuilder.DropColumn(
                name: "CEP",
                table: "EMPRESAS");

            migrationBuilder.DropColumn(
                name: "CIDADE",
                table: "EMPRESAS");

            migrationBuilder.DropColumn(
                name: "CPF_CNPJ",
                table: "EMPRESAS");

            migrationBuilder.DropColumn(
                name: "DT_NASCIMENTO",
                table: "EMPRESAS");

            migrationBuilder.DropColumn(
                name: "EMAIL",
                table: "EMPRESAS");

            migrationBuilder.DropColumn(
                name: "LOGRADOURO",
                table: "EMPRESAS");

            migrationBuilder.DropColumn(
                name: "NM_FANTASIA",
                table: "EMPRESAS");

            migrationBuilder.DropColumn(
                name: "NM_RAZAO",
                table: "EMPRESAS");

            migrationBuilder.DropColumn(
                name: "NR_IERG",
                table: "EMPRESAS");

            migrationBuilder.DropColumn(
                name: "NR_TELEFONE",
                table: "EMPRESAS");

            migrationBuilder.DropColumn(
                name: "NUMERO",
                table: "EMPRESAS");

            migrationBuilder.DropColumn(
                name: "UF",
                table: "EMPRESAS");
        }
    }
}
