using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENGER.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSubscriptionRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ASSINATURA_TIPO_ASSINATURA_SubscriptionTypeId",
                table: "ASSINATURA");

            migrationBuilder.DropIndex(
                name: "IX_ASSINATURA_SubscriptionTypeId",
                table: "ASSINATURA");

            migrationBuilder.DropIndex(
                name: "IX_ASSINATURA_TypeSubscriptionId",
                table: "ASSINATURA");

            migrationBuilder.DropColumn(
                name: "SubscriptionTypeId",
                table: "ASSINATURA");

            migrationBuilder.CreateIndex(
                name: "IX_ASSINATURA_TypeSubscriptionId",
                table: "ASSINATURA",
                column: "TypeSubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ASSINATURA_TypeSubscriptionId",
                table: "ASSINATURA");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionTypeId",
                table: "ASSINATURA",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ASSINATURA_SubscriptionTypeId",
                table: "ASSINATURA",
                column: "SubscriptionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ASSINATURA_TypeSubscriptionId",
                table: "ASSINATURA",
                column: "TypeSubscriptionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ASSINATURA_TIPO_ASSINATURA_SubscriptionTypeId",
                table: "ASSINATURA",
                column: "SubscriptionTypeId",
                principalTable: "TIPO_ASSINATURA",
                principalColumn: "CD_TP_ASSINATURA",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
