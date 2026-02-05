using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENGER.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjustandoNomesTabelaEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "EMPRESAS");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "EMPRESAS",
                newName: "DT_ATUALIZACAO");

            migrationBuilder.RenameColumn(
                name: "SubscriptionCode",
                table: "EMPRESAS",
                newName: "CD_ASSINATURA");

            migrationBuilder.RenameColumn(
                name: "EntryDate",
                table: "EMPRESAS",
                newName: "DT_ENTRADA");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "EMPRESAS",
                newName: "CD_EMPRESA");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EMPRESAS",
                table: "EMPRESAS",
                column: "CD_EMPRESA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EMPRESAS",
                table: "EMPRESAS");

            migrationBuilder.RenameTable(
                name: "EMPRESAS",
                newName: "Companies");

            migrationBuilder.RenameColumn(
                name: "DT_ENTRADA",
                table: "Companies",
                newName: "EntryDate");

            migrationBuilder.RenameColumn(
                name: "DT_ATUALIZACAO",
                table: "Companies",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "CD_ASSINATURA",
                table: "Companies",
                newName: "SubscriptionCode");

            migrationBuilder.RenameColumn(
                name: "CD_EMPRESA",
                table: "Companies",
                newName: "CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "CompanyId");
        }
    }
}
