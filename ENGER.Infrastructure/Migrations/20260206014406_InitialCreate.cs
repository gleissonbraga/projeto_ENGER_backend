using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ENGER.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    DT_PAGAMENTO = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TypeSubscriptionId = table.Column<int>(type: "integer", nullable: false),
                    SubscriptionTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASSINATURA", x => x.CD_ASSINATURA);
                    table.UniqueConstraint("AK_ASSINATURA_CD_CHAVE", x => x.CD_CHAVE);
                    table.ForeignKey(
                        name: "FK_ASSINATURA_TIPO_ASSINATURA_SubscriptionTypeId",
                        column: x => x.SubscriptionTypeId,
                        principalTable: "TIPO_ASSINATURA",
                        principalColumn: "CD_TP_ASSINATURA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ASSINATURA_TIPO_ASSINATURA_TypeSubscriptionId",
                        column: x => x.TypeSubscriptionId,
                        principalTable: "TIPO_ASSINATURA",
                        principalColumn: "CD_TP_ASSINATURA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EMPRESAS",
                columns: table => new
                {
                    CD_EMPRESA = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NM_RAZAO = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NM_FANTASIA = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CPF_CNPJ = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    NR_IERG = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    EMAIL = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LOGRADOURO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NUMERO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CIDADE = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    BAIRRO = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    CEP = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    UF = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    NR_TELEFONE = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    DT_NASCIMENTO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DT_ENTRADA = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DT_ATUALIZACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CD_ASSINATURA = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESAS", x => x.CD_EMPRESA);
                    table.ForeignKey(
                        name: "FK_EMPRESAS_ASSINATURA_CD_ASSINATURA",
                        column: x => x.CD_ASSINATURA,
                        principalTable: "ASSINATURA",
                        principalColumn: "CD_CHAVE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    CD_USUARIO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NM_USUARIO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EMAIL = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    SENHA = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    ADMIN = table.Column<int>(type: "integer", nullable: false),
                    DT_ENTRADA = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 50, nullable: false),
                    DT_ATUALIZACAO = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 50, nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.CD_USUARIO);
                    table.ForeignKey(
                        name: "FK_USUARIOS_EMPRESAS_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "EMPRESAS",
                        principalColumn: "CD_EMPRESA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASSINATURA_SubscriptionTypeId",
                table: "ASSINATURA",
                column: "SubscriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ASSINATURA_TypeSubscriptionId",
                table: "ASSINATURA",
                column: "TypeSubscriptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EMPRESAS_CD_ASSINATURA",
                table: "EMPRESAS",
                column: "CD_ASSINATURA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USUARIOS_CompanyId",
                table: "USUARIOS",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USUARIOS");

            migrationBuilder.DropTable(
                name: "EMPRESAS");

            migrationBuilder.DropTable(
                name: "ASSINATURA");

            migrationBuilder.DropTable(
                name: "TIPO_ASSINATURA");
        }
    }
}
