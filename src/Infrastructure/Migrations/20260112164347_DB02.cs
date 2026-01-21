using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class DB02 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Portfolios",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                sub_category = table.Column<string>(type: "text", nullable: false),
                title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                description = table.Column<string>(type: "text", nullable: false),
                is_featured = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_portfolios", x => x.id));

        migrationBuilder.CreateTable(
            name: "portfolio_image",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                image_path = table.Column<string>(type: "text", nullable: false),
                portfolio_id = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_portfolio_image", x => x.id);
                table.ForeignKey(
                    name: "fk_portfolio_image_portfolios_portfolio_id",
                    column: x => x.portfolio_id,
                    principalSchema: "public",
                    principalTable: "Portfolios",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_portfolio_image_portfolio_id",
            schema: "public",
            table: "portfolio_image",
            column: "portfolio_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "portfolio_image",
            schema: "public");

        migrationBuilder.DropTable(
            name: "Portfolios",
            schema: "public");
    }
}
