using System;
using System.Collections;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Academic.Migrations
{
    public partial class InitialAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "criteriu",
                columns: table => new
                {
                    id_criteriu = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descriere = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("criteriu_pkey", x => x.id_criteriu);
                });

            migrationBuilder.CreateTable(
                name: "facultate",
                columns: table => new
                {
                    id_facultate = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nume = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("facultate_pkey", x => x.id_facultate);
                });

            migrationBuilder.CreateTable(
                name: "materie",
                columns: table => new
                {
                    id_materie = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nume = table.Column<string>(maxLength: 50, nullable: true),
                    cod = table.Column<string>(maxLength: 20, nullable: true),
                    nr_credite = table.Column<int>(nullable: true),
                    descriere = table.Column<string>(maxLength: 100, nullable: true),
                    finalizare = table.Column<string>(maxLength: 2, nullable: true),
                    nr_pachet = table.Column<int>(nullable: true),
                    tip_activitate = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("materie_pkey", x => x.id_materie);
                });

            migrationBuilder.CreateTable(
                name: "sala",
                columns: table => new
                {
                    id_sala = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nume = table.Column<string>(maxLength: 30, nullable: true),
                    locatie = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("sala_pkey", x => x.id_sala);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id_user = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(maxLength: 50, nullable: true),
                    nume = table.Column<string>(maxLength: 50, nullable: true),
                    prenume = table.Column<string>(maxLength: 50, nullable: true),
                    cnp = table.Column<string>(maxLength: 13, nullable: true),
                    tip_utilizator = table.Column<string>(maxLength: 15, nullable: true),
                    p_hash = table.Column<BitArray>(type: "bit varying(255)", nullable: true),
                    p_salt = table.Column<BitArray>(type: "bit varying(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.id_user);
                });

            migrationBuilder.CreateTable(
                name: "profesor",
                columns: table => new
                {
                    id_profesor = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_user = table.Column<int>(nullable: false, defaultValueSql: "nextval('users_id_user_seq'::regclass)"),
                    username = table.Column<string>(maxLength: 50, nullable: true),
                    nume = table.Column<string>(maxLength: 50, nullable: true),
                    prenume = table.Column<string>(maxLength: 50, nullable: true),
                    cnp = table.Column<string>(maxLength: 13, nullable: true),
                    tip_utilizator = table.Column<string>(maxLength: 15, nullable: true),
                    p_hash = table.Column<BitArray>(type: "bit varying(255)", nullable: true),
                    p_salt = table.Column<BitArray>(type: "bit varying(255)", nullable: true),
                    grad = table.Column<string>(maxLength: 15, nullable: true),
                    domeniu = table.Column<string>(maxLength: 20, nullable: true),
                    site = table.Column<string>(maxLength: 50, nullable: true),
                    mail = table.Column<string>(maxLength: 50, nullable: true),
                    id_facultate = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("profesor_pkey", x => x.id_profesor);
                    table.ForeignKey(
                        name: "fk_facultate",
                        column: x => x.id_facultate,
                        principalTable: "facultate",
                        principalColumn: "id_facultate",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "specializare",
                columns: table => new
                {
                    id_specializare = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nume = table.Column<string>(maxLength: 50, nullable: true),
                    cod = table.Column<string>(maxLength: 50, nullable: true),
                    id_facultate = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("specializare_pkey", x => x.id_specializare);
                    table.ForeignKey(
                        name: "fk_facultate",
                        column: x => x.id_facultate,
                        principalTable: "facultate",
                        principalColumn: "id_facultate",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    id_admin = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_user = table.Column<int>(nullable: false, defaultValueSql: "nextval('users_id_user_seq'::regclass)"),
                    username = table.Column<string>(maxLength: 50, nullable: true),
                    nume = table.Column<string>(maxLength: 50, nullable: true),
                    prenume = table.Column<string>(maxLength: 50, nullable: true),
                    cnp = table.Column<string>(maxLength: 13, nullable: true),
                    tip_utilizator = table.Column<string>(maxLength: 15, nullable: true),
                    p_hash = table.Column<BitArray>(type: "bit varying(255)", nullable: true),
                    p_salt = table.Column<BitArray>(type: "bit varying(255)", nullable: true),
                    mail = table.Column<string>(maxLength: 50, nullable: true),
                    id_specializare = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("admin_pkey", x => x.id_admin);
                    table.ForeignKey(
                        name: "fk_specializare",
                        column: x => x.id_specializare,
                        principalTable: "specializare",
                        principalColumn: "id_specializare",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "formatie",
                columns: table => new
                {
                    id_formatie = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_specializare = table.Column<int>(nullable: false),
                    grupa = table.Column<string>(maxLength: 10, nullable: true),
                    semi_grupa = table.Column<string>(maxLength: 10, nullable: true),
                    an_studiu = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_formatie", x => new { x.id_formatie, x.id_specializare });
                    table.ForeignKey(
                        name: "fk_specializare",
                        column: x => x.id_specializare,
                        principalTable: "specializare",
                        principalColumn: "id_specializare",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orarmaterie",
                columns: table => new
                {
                    ora_inceput = table.Column<TimeSpan>(type: "time without time zone", nullable: false),
                    ora_sfarsit = table.Column<TimeSpan>(type: "time without time zone", nullable: false),
                    id_materie = table.Column<int>(nullable: false),
                    id_profesor = table.Column<int>(nullable: false),
                    ziua_saptamanii = table.Column<string>(maxLength: 20, nullable: false),
                    frecventa = table.Column<string>(maxLength: 10, nullable: false),
                    id_formatie = table.Column<int>(nullable: false),
                    id_specializare = table.Column<int>(nullable: false),
                    id_sala = table.Column<int>(nullable: false),
                    data = table.Column<DateTime>(type: "date", nullable: true),
                    tip = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orar_materie", x => new { x.ora_inceput, x.ora_sfarsit, x.id_materie, x.id_profesor, x.ziua_saptamanii, x.frecventa, x.id_formatie, x.id_specializare });
                    table.ForeignKey(
                        name: "fk_materie",
                        column: x => x.id_materie,
                        principalTable: "materie",
                        principalColumn: "id_materie",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_profesor",
                        column: x => x.id_profesor,
                        principalTable: "profesor",
                        principalColumn: "id_profesor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_sala",
                        column: x => x.id_sala,
                        principalTable: "sala",
                        principalColumn: "id_sala",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_formatie",
                        columns: x => new { x.id_formatie, x.id_specializare },
                        principalTable: "formatie",
                        principalColumns: new[] { "id_formatie", "id_specializare" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    id_student = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_user = table.Column<int>(nullable: false, defaultValueSql: "nextval('users_id_user_seq'::regclass)"),
                    username = table.Column<string>(maxLength: 50, nullable: true),
                    nume = table.Column<string>(maxLength: 50, nullable: true),
                    prenume = table.Column<string>(maxLength: 50, nullable: true),
                    cnp = table.Column<string>(maxLength: 13, nullable: true),
                    tip_utilizator = table.Column<string>(maxLength: 15, nullable: true),
                    p_hash = table.Column<BitArray>(type: "bit varying(255)", nullable: true),
                    p_salt = table.Column<BitArray>(type: "bit varying(255)", nullable: true),
                    nr_matricol = table.Column<string>(maxLength: 10, nullable: true),
                    cup = table.Column<string>(maxLength: 10, nullable: true),
                    id_formatie = table.Column<int>(nullable: true),
                    id_specializare = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("student_pkey", x => x.id_student);
                    table.ForeignKey(
                        name: "fk_formatie",
                        columns: x => new { x.id_formatie, x.id_specializare },
                        principalTable: "formatie",
                        principalColumns: new[] { "id_formatie", "id_specializare" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contractdestudiu",
                columns: table => new
                {
                    id_student = table.Column<int>(nullable: false),
                    an_de_studiu = table.Column<int>(nullable: false),
                    an_calendaristic = table.Column<string>(maxLength: 50, nullable: false),
                    cod = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contract_de_studiu", x => new { x.id_student, x.an_de_studiu, x.an_calendaristic });
                    table.ForeignKey(
                        name: "fk_student",
                        column: x => x.id_student,
                        principalTable: "student",
                        principalColumn: "id_student",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "detaliucontract",
                columns: table => new
                {
                    id_materie = table.Column<int>(nullable: false),
                    id_student = table.Column<int>(nullable: false),
                    an_de_studiu = table.Column<int>(nullable: false),
                    an_calendaristic = table.Column<string>(maxLength: 50, nullable: false),
                    nota = table.Column<int>(nullable: true),
                    nota_restanta = table.Column<int>(nullable: true),
                    promovata = table.Column<bool>(nullable: true),
                    data_promovarii = table.Column<DateTime>(type: "date", nullable: true),
                    data_examen = table.Column<DateTime>(type: "date", nullable: true),
                    data_restanta = table.Column<DateTime>(type: "date", nullable: true),
                    semestru = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_detaliu_contract", x => new { x.id_materie, x.id_student, x.an_de_studiu, x.an_calendaristic });
                    table.ForeignKey(
                        name: "fk_materie",
                        column: x => x.id_materie,
                        principalTable: "materie",
                        principalColumn: "id_materie",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_contract_de_studiu",
                        columns: x => new { x.id_student, x.an_de_studiu, x.an_calendaristic },
                        principalTable: "contractdestudiu",
                        principalColumns: new[] { "id_student", "an_de_studiu", "an_calendaristic" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "review",
                columns: table => new
                {
                    id_profesor = table.Column<int>(nullable: false),
                    id_materie = table.Column<int>(nullable: false),
                    id_criteriu = table.Column<int>(nullable: false),
                    id_student = table.Column<int>(nullable: false),
                    an_de_studiu = table.Column<int>(nullable: false),
                    an_caledaristic = table.Column<string>(maxLength: 50, nullable: false),
                    nota = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_review", x => new { x.id_profesor, x.id_materie, x.id_criteriu, x.id_student, x.an_de_studiu, x.an_caledaristic });
                    table.ForeignKey(
                        name: "fk_criteriu",
                        column: x => x.id_criteriu,
                        principalTable: "criteriu",
                        principalColumn: "id_criteriu",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_profesor",
                        column: x => x.id_profesor,
                        principalTable: "profesor",
                        principalColumn: "id_profesor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_detaliu_contract",
                        columns: x => new { x.id_materie, x.id_student, x.an_de_studiu, x.an_caledaristic },
                        principalTable: "detaliucontract",
                        principalColumns: new[] { "id_materie", "id_student", "an_de_studiu", "an_calendaristic" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_admin_id_specializare",
                table: "admin",
                column: "id_specializare");

            migrationBuilder.CreateIndex(
                name: "IX_detaliucontract_id_student_an_de_studiu_an_calendaristic",
                table: "detaliucontract",
                columns: new[] { "id_student", "an_de_studiu", "an_calendaristic" });

            migrationBuilder.CreateIndex(
                name: "IX_formatie_id_specializare",
                table: "formatie",
                column: "id_specializare");

            migrationBuilder.CreateIndex(
                name: "IX_orarmaterie_id_materie",
                table: "orarmaterie",
                column: "id_materie");

            migrationBuilder.CreateIndex(
                name: "IX_orarmaterie_id_profesor",
                table: "orarmaterie",
                column: "id_profesor");

            migrationBuilder.CreateIndex(
                name: "IX_orarmaterie_id_sala",
                table: "orarmaterie",
                column: "id_sala");

            migrationBuilder.CreateIndex(
                name: "IX_orarmaterie_id_formatie_id_specializare",
                table: "orarmaterie",
                columns: new[] { "id_formatie", "id_specializare" });

            migrationBuilder.CreateIndex(
                name: "IX_profesor_id_facultate",
                table: "profesor",
                column: "id_facultate");

            migrationBuilder.CreateIndex(
                name: "IX_review_id_criteriu",
                table: "review",
                column: "id_criteriu");

            migrationBuilder.CreateIndex(
                name: "IX_review_id_materie_id_student_an_de_studiu_an_caledaristic",
                table: "review",
                columns: new[] { "id_materie", "id_student", "an_de_studiu", "an_caledaristic" });

            migrationBuilder.CreateIndex(
                name: "IX_specializare_id_facultate",
                table: "specializare",
                column: "id_facultate");

            migrationBuilder.CreateIndex(
                name: "IX_student_id_formatie_id_specializare",
                table: "student",
                columns: new[] { "id_formatie", "id_specializare" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "orarmaterie");

            migrationBuilder.DropTable(
                name: "review");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "sala");

            migrationBuilder.DropTable(
                name: "criteriu");

            migrationBuilder.DropTable(
                name: "profesor");

            migrationBuilder.DropTable(
                name: "detaliucontract");

            migrationBuilder.DropTable(
                name: "materie");

            migrationBuilder.DropTable(
                name: "contractdestudiu");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "formatie");

            migrationBuilder.DropTable(
                name: "specializare");

            migrationBuilder.DropTable(
                name: "facultate");
        }
    }
}
