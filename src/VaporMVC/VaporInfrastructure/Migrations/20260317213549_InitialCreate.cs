using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VaporInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Genres",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("Genres_pkey", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Publishers",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("Publishers_pkey", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Statuses",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("Statuses_pkey", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Users",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
            //        Email = table.Column<string>(type: "character(100)", fixedLength: true, maxLength: 100, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("Users_pkey", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Games",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        PublisherId = table.Column<int>(type: "integer", nullable: false),
            //        Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
            //        IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
            //        Description = table.Column<string>(type: "text", nullable: true),
            //        Price = table.Column<decimal>(type: "numeric(10,2)", precision: 9, scale: 2, nullable: false),
            //        ReleasedDate = table.Column<DateOnly>(type: "date", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("Games_pkey", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Game_Publisher",
            //            column: x => x.PublisherId,
            //            principalTable: "Publishers",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Orders",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        UserId = table.Column<int>(type: "integer", nullable: false),
            //        StatusId = table.Column<int>(type: "integer", nullable: false),
            //        CreatedDate = table.Column<DateOnly>(type: "date", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("Orders_pkey", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Order_Status",
            //            column: x => x.StatusId,
            //            principalTable: "Statuses",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Order_User",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "GameGenres",
            //    columns: table => new
            //    {
            //        GameId = table.Column<int>(type: "integer", nullable: false),
            //        GenreId = table.Column<int>(type: "integer", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("GameGenres_pkey", x => new { x.GameId, x.GenreId });
            //        table.ForeignKey(
            //            name: "FK_GameGenre_Game",
            //            column: x => x.GameId,
            //            principalTable: "Games",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_GameGenre_Genre",
            //            column: x => x.GenreId,
            //            principalTable: "Genres",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PriceHistories",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        GameId = table.Column<int>(type: "integer", nullable: false),
            //        OldPrice = table.Column<decimal>(type: "numeric(10,2)", precision: 9, scale: 2, nullable: false),
            //        NewPrice = table.Column<decimal>(type: "numeric(10,2)", precision: 9, scale: 2, nullable: false),
            //        ChangedData = table.Column<DateOnly>(type: "date", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PriceHistories_pkey", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_PriceHistory_Game",
            //            column: x => x.GameId,
            //            principalTable: "Games",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Reviews",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        GameId = table.Column<int>(type: "integer", nullable: false),
            //        UserId = table.Column<int>(type: "integer", nullable: false),
            //        Content = table.Column<string>(type: "text", nullable: false),
            //        Raiting = table.Column<int>(type: "integer", nullable: false),
            //        CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("Reviews_pkey", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Review_Game",
            //            column: x => x.GameId,
            //            principalTable: "Games",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Review_User",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "OrderItems",
            //    columns: table => new
            //    {
            //        GameId = table.Column<int>(type: "integer", nullable: false),
            //        OrderId = table.Column<int>(type: "integer", nullable: false),
            //        Price = table.Column<decimal>(type: "numeric(9,2)", precision: 9, scale: 2, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("OrderItems_pkey", x => new { x.GameId, x.OrderId });
            //        table.ForeignKey(
            //            name: "FK_OrderItem_Game",
            //            column: x => x.GameId,
            //            principalTable: "Games",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_OrderItem_Order",
            //            column: x => x.OrderId,
            //            principalTable: "Orders",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_GameGenres_GenreId",
            //    table: "GameGenres",
            //    column: "GenreId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Games_PublisherId",
            //    table: "Games",
            //    column: "PublisherId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderItems_OrderId",
            //    table: "OrderItems",
            //    column: "OrderId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Orders_StatusId",
            //    table: "Orders",
            //    column: "StatusId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Orders_UserId",
            //    table: "Orders",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PriceHistories_GameId",
            //    table: "PriceHistories",
            //    column: "GameId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Reviews_GameId",
            //    table: "Reviews",
            //    column: "GameId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Reviews_UserId",
            //    table: "Reviews",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "UQ_SatusName",
            //    table: "Statuses",
            //    column: "Name",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "UQ_Username",
            //    table: "Users",
            //    column: "Username",
            //    unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGenres");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "PriceHistories");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
