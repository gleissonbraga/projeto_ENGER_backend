using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ENGER.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BudgetAndConstruction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CHAVE_ORCAMENTO",
                table: "ORCAMENTO",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "DS_LOGRADOURO",
                table: "ORCAMENTO",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NM_BAIRRO",
                table: "ORCAMENTO",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NM_CIDADE",
                table: "ORCAMENTO",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NM_ESTADO",
                table: "ORCAMENTO",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NR_CEP",
                table: "ORCAMENTO",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NR_LOGRADOURO",
                table: "ORCAMENTO",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SG_UF",
                table: "ORCAMENTO",
                type: "character varying(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OBRAS",
                columns: table => new
                {
                    CD_OBRA = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DS_OBRA = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CD_ORCAMENTO = table.Column<int>(type: "integer", nullable: true),
                    VL_TOTAL_PAGO = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    VL_TOTAL_OBRA = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    LOGRADOURO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    NR_LOGRADOURO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CIDADE = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    BAIRRO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CEP = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    UF = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    DS_ESTADO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DT_INICIO = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DT_ENTREGA_PREVISTA = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DT_FINALIZACAO = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    STATUS_OBRA = table.Column<int>(type: "integer", nullable: false),
                    CD_EMPRESA = table.Column<int>(type: "integer", nullable: true),
                    CD_RESPONSAVEL = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OBRAS", x => x.CD_OBRA);
                    table.ForeignKey(
                        name: "FK_OBRAS_EMPRESAS_CD_EMPRESA",
                        column: x => x.CD_EMPRESA,
                        principalTable: "EMPRESAS",
                        principalColumn: "CD_EMPRESA",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OBRAS_ORCAMENTO_CD_ORCAMENTO",
                        column: x => x.CD_ORCAMENTO,
                        principalTable: "ORCAMENTO",
                        principalColumn: "CD_ORCAMENTO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentType",
                columns: table => new
                {
                    PaymentTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.PaymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ALUGUEIS",
                columns: table => new
                {
                    CD_ALUGUEL = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DS_EQUIPAMENTO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    VL_ALUGUEL = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    NR_DIAS = table.Column<int>(type: "integer", nullable: true),
                    DT_ENTRADA = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DT_SAIDA = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NM_RECEBIDO_POR = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    NM_DEVOLVIDO_POR = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CD_OBRA = table.Column<int>(type: "integer", nullable: true),
                    BudgetId = table.Column<int>(type: "integer", nullable: true),
                    ConstructionId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALUGUEIS", x => x.CD_ALUGUEL);
                    table.ForeignKey(
                        name: "FK_ALUGUEIS_OBRAS_CD_OBRA",
                        column: x => x.CD_OBRA,
                        principalTable: "OBRAS",
                        principalColumn: "CD_OBRA");
                    table.ForeignKey(
                        name: "FK_ALUGUEIS_OBRAS_ConstructionId1",
                        column: x => x.ConstructionId1,
                        principalTable: "OBRAS",
                        principalColumn: "CD_OBRA");
                    table.ForeignKey(
                        name: "FK_ALUGUEIS_ORCAMENTO_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "ORCAMENTO",
                        principalColumn: "CD_ORCAMENTO");
                });

            migrationBuilder.CreateTable(
                name: "ConstructionStages",
                columns: table => new
                {
                    StageId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: true),
                    ConstructionId = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    BudgetId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionStages", x => x.StageId);
                    table.ForeignKey(
                        name: "FK_ConstructionStages_OBRAS_ConstructionId",
                        column: x => x.ConstructionId,
                        principalTable: "OBRAS",
                        principalColumn: "CD_OBRA");
                    table.ForeignKey(
                        name: "FK_ConstructionStages_ORCAMENTO_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "ORCAMENTO",
                        principalColumn: "CD_ORCAMENTO");
                });

            migrationBuilder.CreateTable(
                name: "OBRA_ANEXOS",
                columns: table => new
                {
                    CD_OBRA_ANEXO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DS_ANEXO = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    URL_IMAGEM = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CD_OBRA = table.Column<int>(type: "integer", nullable: true),
                    BudgetId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OBRA_ANEXOS", x => x.CD_OBRA_ANEXO);
                    table.ForeignKey(
                        name: "FK_OBRA_ANEXOS_OBRAS_CD_OBRA",
                        column: x => x.CD_OBRA,
                        principalTable: "OBRAS",
                        principalColumn: "CD_OBRA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OBRA_ANEXOS_ORCAMENTO_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "ORCAMENTO",
                        principalColumn: "CD_ORCAMENTO");
                });

            migrationBuilder.CreateTable(
                name: "OBRA_PRESENCA",
                columns: table => new
                {
                    CD_PRESENCA = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CD_FUNCIONARIO = table.Column<int>(type: "integer", nullable: true),
                    CD_OBRA = table.Column<int>(type: "integer", nullable: true),
                    CD_USUARIO = table.Column<int>(type: "integer", nullable: true),
                    DT_PRESENCA = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IN_PRESENTE = table.Column<bool>(type: "boolean", nullable: false),
                    BudgetId = table.Column<int>(type: "integer", nullable: true),
                    ConstructionId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OBRA_PRESENCA", x => x.CD_PRESENCA);
                    table.ForeignKey(
                        name: "FK_OBRA_PRESENCA_FUNCIONARIOS_CD_FUNCIONARIO",
                        column: x => x.CD_FUNCIONARIO,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "CD_FUNCIONARIO");
                    table.ForeignKey(
                        name: "FK_OBRA_PRESENCA_OBRAS_CD_OBRA",
                        column: x => x.CD_OBRA,
                        principalTable: "OBRAS",
                        principalColumn: "CD_OBRA");
                    table.ForeignKey(
                        name: "FK_OBRA_PRESENCA_OBRAS_ConstructionId1",
                        column: x => x.ConstructionId1,
                        principalTable: "OBRAS",
                        principalColumn: "CD_OBRA");
                    table.ForeignKey(
                        name: "FK_OBRA_PRESENCA_ORCAMENTO_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "ORCAMENTO",
                        principalColumn: "CD_ORCAMENTO");
                    table.ForeignKey(
                        name: "FK_OBRA_PRESENCA_USUARIOS_CD_USUARIO",
                        column: x => x.CD_USUARIO,
                        principalTable: "USUARIOS",
                        principalColumn: "CD_USUARIO");
                });

            migrationBuilder.CreateTable(
                name: "OBRAS_FUNCIONARIOS",
                columns: table => new
                {
                    CD_OBRA_FUNCIONARIO = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CD_FUNCIONARIO = table.Column<int>(type: "integer", nullable: true),
                    CD_OBRA = table.Column<int>(type: "integer", nullable: true),
                    BudgetId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OBRAS_FUNCIONARIOS", x => x.CD_OBRA_FUNCIONARIO);
                    table.ForeignKey(
                        name: "FK_OBRAS_FUNCIONARIOS_FUNCIONARIOS_CD_FUNCIONARIO",
                        column: x => x.CD_FUNCIONARIO,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "CD_FUNCIONARIO");
                    table.ForeignKey(
                        name: "FK_OBRAS_FUNCIONARIOS_OBRAS_CD_OBRA",
                        column: x => x.CD_OBRA,
                        principalTable: "OBRAS",
                        principalColumn: "CD_OBRA");
                    table.ForeignKey(
                        name: "FK_OBRAS_FUNCIONARIOS_ORCAMENTO_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "ORCAMENTO",
                        principalColumn: "CD_ORCAMENTO");
                });

            migrationBuilder.CreateTable(
                name: "OBRAS_PAGAMENTO",
                columns: table => new
                {
                    CD_PAG_OBRA = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DT_PAGAMENTO = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CD_TP_PAGAMENTO = table.Column<int>(type: "integer", nullable: true),
                    CD_OBRA = table.Column<int>(type: "integer", nullable: true),
                    CD_ETAPA = table.Column<int>(type: "integer", nullable: true),
                    VL_PAGAMENTO = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    BudgetId = table.Column<int>(type: "integer", nullable: true),
                    ConstructionId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OBRAS_PAGAMENTO", x => x.CD_PAG_OBRA);
                    table.ForeignKey(
                        name: "FK_OBRAS_PAGAMENTO_OBRAS_CD_OBRA",
                        column: x => x.CD_OBRA,
                        principalTable: "OBRAS",
                        principalColumn: "CD_OBRA");
                    table.ForeignKey(
                        name: "FK_OBRAS_PAGAMENTO_OBRAS_ConstructionId1",
                        column: x => x.ConstructionId1,
                        principalTable: "OBRAS",
                        principalColumn: "CD_OBRA");
                    table.ForeignKey(
                        name: "FK_OBRAS_PAGAMENTO_ORCAMENTO_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "ORCAMENTO",
                        principalColumn: "CD_ORCAMENTO");
                    table.ForeignKey(
                        name: "FK_OBRAS_PAGAMENTO_PaymentType_CD_TP_PAGAMENTO",
                        column: x => x.CD_TP_PAGAMENTO,
                        principalTable: "PaymentType",
                        principalColumn: "PaymentTypeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ALUGUEIS_BudgetId",
                table: "ALUGUEIS",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_ALUGUEIS_CD_OBRA",
                table: "ALUGUEIS",
                column: "CD_OBRA");

            migrationBuilder.CreateIndex(
                name: "IX_ALUGUEIS_ConstructionId1",
                table: "ALUGUEIS",
                column: "ConstructionId1");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionStages_BudgetId",
                table: "ConstructionStages",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionStages_ConstructionId",
                table: "ConstructionStages",
                column: "ConstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_OBRA_ANEXOS_BudgetId",
                table: "OBRA_ANEXOS",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_OBRA_ANEXOS_CD_OBRA",
                table: "OBRA_ANEXOS",
                column: "CD_OBRA");

            migrationBuilder.CreateIndex(
                name: "IX_OBRA_PRESENCA_BudgetId",
                table: "OBRA_PRESENCA",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_OBRA_PRESENCA_CD_FUNCIONARIO",
                table: "OBRA_PRESENCA",
                column: "CD_FUNCIONARIO");

            migrationBuilder.CreateIndex(
                name: "IX_OBRA_PRESENCA_CD_OBRA",
                table: "OBRA_PRESENCA",
                column: "CD_OBRA");

            migrationBuilder.CreateIndex(
                name: "IX_OBRA_PRESENCA_CD_USUARIO",
                table: "OBRA_PRESENCA",
                column: "CD_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_OBRA_PRESENCA_ConstructionId1",
                table: "OBRA_PRESENCA",
                column: "ConstructionId1");

            migrationBuilder.CreateIndex(
                name: "IX_OBRAS_CD_EMPRESA",
                table: "OBRAS",
                column: "CD_EMPRESA");

            migrationBuilder.CreateIndex(
                name: "IX_OBRAS_CD_ORCAMENTO",
                table: "OBRAS",
                column: "CD_ORCAMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_OBRAS_FUNCIONARIOS_BudgetId",
                table: "OBRAS_FUNCIONARIOS",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_OBRAS_FUNCIONARIOS_CD_FUNCIONARIO",
                table: "OBRAS_FUNCIONARIOS",
                column: "CD_FUNCIONARIO");

            migrationBuilder.CreateIndex(
                name: "IX_OBRAS_FUNCIONARIOS_CD_OBRA",
                table: "OBRAS_FUNCIONARIOS",
                column: "CD_OBRA");

            migrationBuilder.CreateIndex(
                name: "IX_OBRAS_PAGAMENTO_BudgetId",
                table: "OBRAS_PAGAMENTO",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_OBRAS_PAGAMENTO_CD_OBRA",
                table: "OBRAS_PAGAMENTO",
                column: "CD_OBRA");

            migrationBuilder.CreateIndex(
                name: "IX_OBRAS_PAGAMENTO_CD_TP_PAGAMENTO",
                table: "OBRAS_PAGAMENTO",
                column: "CD_TP_PAGAMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_OBRAS_PAGAMENTO_ConstructionId1",
                table: "OBRAS_PAGAMENTO",
                column: "ConstructionId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ALUGUEIS");

            migrationBuilder.DropTable(
                name: "ConstructionStages");

            migrationBuilder.DropTable(
                name: "OBRA_ANEXOS");

            migrationBuilder.DropTable(
                name: "OBRA_PRESENCA");

            migrationBuilder.DropTable(
                name: "OBRAS_FUNCIONARIOS");

            migrationBuilder.DropTable(
                name: "OBRAS_PAGAMENTO");

            migrationBuilder.DropTable(
                name: "OBRAS");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropColumn(
                name: "CHAVE_ORCAMENTO",
                table: "ORCAMENTO");

            migrationBuilder.DropColumn(
                name: "DS_LOGRADOURO",
                table: "ORCAMENTO");

            migrationBuilder.DropColumn(
                name: "NM_BAIRRO",
                table: "ORCAMENTO");

            migrationBuilder.DropColumn(
                name: "NM_CIDADE",
                table: "ORCAMENTO");

            migrationBuilder.DropColumn(
                name: "NM_ESTADO",
                table: "ORCAMENTO");

            migrationBuilder.DropColumn(
                name: "NR_CEP",
                table: "ORCAMENTO");

            migrationBuilder.DropColumn(
                name: "NR_LOGRADOURO",
                table: "ORCAMENTO");

            migrationBuilder.DropColumn(
                name: "SG_UF",
                table: "ORCAMENTO");
        }
    }
}
