using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENGER.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SendEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ENVIA_EMAIL",
                columns: table => new
                {
                    ID_EMAIL = table.Column<Guid>(type: "uuid", nullable: false),
                    DS_DESTINATARIO = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    DS_ASSUNTO = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DS_CORPO = table.Column<string>(type: "text", nullable: false),
                    BL_ANEXO = table.Column<byte[]>(type: "bytea", nullable: true),
                    NM_ARQUIVO = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    DT_ENVIO = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    DT_GRAVACAO = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    STATUS = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENVIA_EMAIL", x => x.ID_EMAIL);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ENVIA_EMAIL");
        }
    }
}
