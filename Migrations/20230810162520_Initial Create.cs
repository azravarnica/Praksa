using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    ImdbID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Plot = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Rated = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Released = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Runtime = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Writer = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Actors = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.ImdbID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
