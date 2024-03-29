﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstimativaColheita.Migrations
{
    public partial class CreateInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoInterno = table.Column<int>(type: "int", nullable: false),
                    Propriedade = table.Column<string>(type: "varchar(200)", nullable: false),
                    Titular = table.Column<string>(type: "varchar(200)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstimativaMotivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstimativaMotivos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FiscaisCampo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoInterno = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Apelido = table.Column<string>(type: "varchar(20)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscaisCampo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposLancamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposLancamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Variedades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoInterno = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(50)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variedades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Encarregados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoInterno = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    IdFiscalCampo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encarregados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encarregados_FiscaisCampo_IdFiscalCampo",
                        column: x => x.IdFiscalCampo,
                        principalTable: "FiscaisCampo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Talhoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoInterno = table.Column<int>(type: "int", nullable: false),
                    AnoPlantio = table.Column<int>(type: "int", nullable: false),
                    QuantidadePes = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    IdContrato = table.Column<int>(type: "int", nullable: false),
                    IdVariedade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talhoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Talhoes_Contratos_IdContrato",
                        column: x => x.IdContrato,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Talhoes_Variedades_IdVariedade",
                        column: x => x.IdVariedade,
                        principalTable: "Variedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ColheitasRealizadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataLancamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Caixas = table.Column<int>(type: "int", nullable: false),
                    IdContrato = table.Column<int>(type: "int", nullable: false),
                    IdTalhao = table.Column<int>(type: "int", nullable: false),
                    IdEncarregado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColheitasRealizadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColheitasRealizadas_Contratos_IdContrato",
                        column: x => x.IdContrato,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ColheitasRealizadas_Encarregados_IdEncarregado",
                        column: x => x.IdEncarregado,
                        principalTable: "Encarregados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ColheitasRealizadas_Talhoes_IdTalhao",
                        column: x => x.IdTalhao,
                        principalTable: "Talhoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EstimativasColheita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataLancamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Caixas = table.Column<int>(type: "int", nullable: false),
                    IdContrato = table.Column<int>(type: "int", nullable: false),
                    IdTalhao = table.Column<int>(type: "int", nullable: false),
                    IdEncarregado = table.Column<int>(type: "int", nullable: false),
                    IdEstimativaMotivo = table.Column<int>(type: "int", nullable: false),
                    IdTipoLancamento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstimativasColheita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstimativasColheita_Contratos_IdContrato",
                        column: x => x.IdContrato,
                        principalTable: "Contratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EstimativasColheita_Encarregados_IdEncarregado",
                        column: x => x.IdEncarregado,
                        principalTable: "Encarregados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EstimativasColheita_EstimativaMotivos_IdEstimativaMotivo",
                        column: x => x.IdEstimativaMotivo,
                        principalTable: "EstimativaMotivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EstimativasColheita_Talhoes_IdTalhao",
                        column: x => x.IdTalhao,
                        principalTable: "Talhoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EstimativasColheita_TiposLancamento_IdTipoLancamento",
                        column: x => x.IdTipoLancamento,
                        principalTable: "TiposLancamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "EstimativaMotivos",
                columns: new[] { "Id", "Ativo", "Descricao" },
                values: new object[] { 1, true, "APONTAMENTO DE CAMPO" });

            migrationBuilder.InsertData(
                table: "TiposLancamento",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 1, "ESTIMADO" });

            migrationBuilder.InsertData(
                table: "TiposLancamento",
                columns: new[] { "Id", "Descricao" },
                values: new object[] { 2, "COLHIDO" });

            migrationBuilder.CreateIndex(
                name: "IX_ColheitasRealizadas_IdContrato",
                table: "ColheitasRealizadas",
                column: "IdContrato");

            migrationBuilder.CreateIndex(
                name: "IX_ColheitasRealizadas_IdEncarregado",
                table: "ColheitasRealizadas",
                column: "IdEncarregado");

            migrationBuilder.CreateIndex(
                name: "IX_ColheitasRealizadas_IdTalhao",
                table: "ColheitasRealizadas",
                column: "IdTalhao");

            migrationBuilder.CreateIndex(
                name: "IX_Encarregados_IdFiscalCampo",
                table: "Encarregados",
                column: "IdFiscalCampo");

            migrationBuilder.CreateIndex(
                name: "IX_EstimativasColheita_IdContrato",
                table: "EstimativasColheita",
                column: "IdContrato");

            migrationBuilder.CreateIndex(
                name: "IX_EstimativasColheita_IdEncarregado",
                table: "EstimativasColheita",
                column: "IdEncarregado");

            migrationBuilder.CreateIndex(
                name: "IX_EstimativasColheita_IdEstimativaMotivo",
                table: "EstimativasColheita",
                column: "IdEstimativaMotivo");

            migrationBuilder.CreateIndex(
                name: "IX_EstimativasColheita_IdTalhao",
                table: "EstimativasColheita",
                column: "IdTalhao");

            migrationBuilder.CreateIndex(
                name: "IX_EstimativasColheita_IdTipoLancamento",
                table: "EstimativasColheita",
                column: "IdTipoLancamento");

            migrationBuilder.CreateIndex(
                name: "IX_Talhoes_IdContrato",
                table: "Talhoes",
                column: "IdContrato");

            migrationBuilder.CreateIndex(
                name: "IX_Talhoes_IdVariedade",
                table: "Talhoes",
                column: "IdVariedade");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColheitasRealizadas");

            migrationBuilder.DropTable(
                name: "EstimativasColheita");

            migrationBuilder.DropTable(
                name: "Encarregados");

            migrationBuilder.DropTable(
                name: "EstimativaMotivos");

            migrationBuilder.DropTable(
                name: "Talhoes");

            migrationBuilder.DropTable(
                name: "TiposLancamento");

            migrationBuilder.DropTable(
                name: "FiscaisCampo");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Variedades");
        }
    }
}
