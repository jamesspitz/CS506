using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WudFilmApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: false),
                    Subcommittee = table.Column<int>(nullable: false),
                    Runtime = table.Column<DateTime>(nullable: false),
                    Format = table.Column<string>(nullable: true),
                    CCAP = table.Column<string>(nullable: true),
                    Trailers = table.Column<string>(nullable: true),
                    PosterUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Showtime",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MovieId = table.Column<int>(nullable: false),
                    ShowDateTime = table.Column<DateTime>(nullable: false),
                    BookingConfirmed = table.Column<bool>(nullable: false),
                    PublicityFormSent = table.Column<bool>(nullable: false),
                    WUDFilm = table.Column<bool>(nullable: false),
                    FBEventMade = table.Column<bool>(nullable: false),
                    PointPerson = table.Column<string>(nullable: true),
                    Projectionist = table.Column<string>(nullable: true),
                    Distributor = table.Column<string>(nullable: true),
                    Format = table.Column<string>(nullable: true),
                    MediaOrdered = table.Column<bool>(nullable: false),
                    MediaArrived = table.Column<DateTime>(nullable: false),
                    MediaReturned = table.Column<DateTime>(nullable: false),
                    MediaReturnTrackingNumber = table.Column<string>(nullable: true),
                    Collaborator = table.Column<string>(nullable: true),
                    CollaboratorPerson = table.Column<string>(nullable: true),
                    CollaboratorContactInfo = table.Column<string>(nullable: true),
                    ShowingAttendence = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showtime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Showtime_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Showtime_MovieId",
                table: "Showtime",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Showtime");

            migrationBuilder.DropTable(
                name: "Movie");
        }
    }
}
