using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Academic.Entities
{
    public partial class academicContext : DbContext
    {
        public academicContext()
        {
        }

        public academicContext(DbContextOptions<academicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Contractdestudiu> Contractdestudiu { get; set; }
        public virtual DbSet<Criteriu> Criteriu { get; set; }
        public virtual DbSet<Departament> Departament { get; set; }
        public virtual DbSet<Detaliucontract> Detaliucontract { get; set; }
        public virtual DbSet<Facultate> Facultate { get; set; }
        public virtual DbSet<Formatie> Formatie { get; set; }
        public virtual DbSet<Materie> Materie { get; set; }
        public virtual DbSet<MaterieSpecializare> MaterieSpecializare { get; set; }
        public virtual DbSet<Orarmaterie> Orarmaterie { get; set; }
        public virtual DbSet<Profesor> Profesor { get; set; }
        public virtual DbSet<Regulament> Regulament { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Sala> Sala { get; set; }
        public virtual DbSet<Specializare> Specializare { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=academic;Username=postgres;Password=BiggieSmalls");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("admin_pkey");

                entity.ToTable("admin");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasDefaultValueSql("nextval('users_id_user_seq'::regclass)");

                entity.Property(e => e.Cnp)
                    .HasColumnName("cnp")
                    .HasMaxLength(13);

                entity.Property(e => e.IdSpecializare).HasColumnName("id_specializare");

                entity.Property(e => e.Mail)
                    .HasColumnName("mail")
                    .HasMaxLength(50);

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(50);

                entity.Property(e => e.PHash).HasColumnName("p_hash");

                entity.Property(e => e.PSalt).HasColumnName("p_salt");

                entity.Property(e => e.Prenume)
                    .HasColumnName("prenume")
                    .HasMaxLength(50);

                entity.Property(e => e.TipUtilizator)
                    .HasColumnName("tip_utilizator")
                    .HasMaxLength(15);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdSpecializareNavigation)
                    .WithMany(p => p.Admin)
                    .HasForeignKey(d => d.IdSpecializare)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_specializare");
            });

            modelBuilder.Entity<Contractdestudiu>(entity =>
            {
                entity.HasKey(e => new { e.IdStudent, e.AnDeStudiu, e.AnCalendaristic })
                    .HasName("pk_contract_de_studiu");

                entity.ToTable("contractdestudiu");

                entity.Property(e => e.IdStudent).HasColumnName("id_student");

                entity.Property(e => e.AnDeStudiu).HasColumnName("an_de_studiu");

                entity.Property(e => e.AnCalendaristic)
                    .HasColumnName("an_calendaristic")
                    .HasMaxLength(50);

                entity.Property(e => e.Cod)
                    .HasColumnName("cod")
                    .HasMaxLength(50);

                entity.Property(e => e.FormaBugetare)
                    .HasColumnName("forma_bugetare")
                    .HasMaxLength(30);

                entity.Property(e => e.Frecventa)
                    .HasColumnName("frecventa")
                    .HasMaxLength(10);

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.Contractdestudiu)
                    .HasForeignKey(d => d.IdStudent)
                    .HasConstraintName("fk_student");
            });

            modelBuilder.Entity<Criteriu>(entity =>
            {
                entity.HasKey(e => e.IdCriteriu)
                    .HasName("criteriu_pkey");

                entity.ToTable("criteriu");

                entity.Property(e => e.IdCriteriu).HasColumnName("id_criteriu");

                entity.Property(e => e.Descriere)
                    .HasColumnName("descriere")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Departament>(entity =>
            {
                entity.HasKey(e => e.IdDepartament)
                    .HasName("departament_pkey");

                entity.ToTable("departament");

                entity.Property(e => e.IdDepartament).HasColumnName("id_departament");

                entity.Property(e => e.IdFacultate).HasColumnName("id_facultate");

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdFacultateNavigation)
                    .WithMany(p => p.Departament)
                    .HasForeignKey(d => d.IdFacultate)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_facultate");
            });

            modelBuilder.Entity<Detaliucontract>(entity =>
            {
                entity.HasKey(e => new { e.IdMaterie, e.IdStudent, e.AnDeStudiu, e.AnCalendaristic })
                    .HasName("pk_detaliu_contract");

                entity.ToTable("detaliucontract");

                entity.Property(e => e.IdMaterie).HasColumnName("id_materie");

                entity.Property(e => e.IdStudent).HasColumnName("id_student");

                entity.Property(e => e.AnDeStudiu).HasColumnName("an_de_studiu");

                entity.Property(e => e.AnCalendaristic)
                    .HasColumnName("an_calendaristic")
                    .HasMaxLength(50);

                entity.Property(e => e.DataExamen)
                    .HasColumnName("data_examen")
                    .HasColumnType("date");

                entity.Property(e => e.DataPromovarii)
                    .HasColumnName("data_promovarii")
                    .HasColumnType("date");

                entity.Property(e => e.DataRestanta)
                    .HasColumnName("data_restanta")
                    .HasColumnType("date");

                entity.Property(e => e.Nota).HasColumnName("nota");

                entity.Property(e => e.NotaRestanta).HasColumnName("nota_restanta");

                entity.Property(e => e.Promovata).HasColumnName("promovata");

                entity.Property(e => e.Semestru).HasColumnName("semestru");

                entity.HasOne(d => d.IdMaterieNavigation)
                    .WithMany(p => p.Detaliucontract)
                    .HasForeignKey(d => d.IdMaterie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_materie");

                entity.HasOne(d => d.Contractdestudiu)
                    .WithMany(p => p.Detaliucontract)
                    .HasForeignKey(d => new { d.IdStudent, d.AnDeStudiu, d.AnCalendaristic })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_contract_de_studiu");
            });

            modelBuilder.Entity<Facultate>(entity =>
            {
                entity.HasKey(e => e.IdFacultate)
                    .HasName("facultate_pkey");

                entity.ToTable("facultate");

                entity.Property(e => e.IdFacultate).HasColumnName("id_facultate");

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Formatie>(entity =>
            {
                entity.HasKey(e => new { e.IdFormatie, e.IdSpecializare })
                    .HasName("pk_formatie");

                entity.ToTable("formatie");

                entity.Property(e => e.IdFormatie)
                    .HasColumnName("id_formatie")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdSpecializare).HasColumnName("id_specializare");

                entity.Property(e => e.AnStudiu)
                    .HasColumnName("an_studiu")
                    .HasMaxLength(10);

                entity.Property(e => e.Grupa)
                    .HasColumnName("grupa")
                    .HasMaxLength(10);

                entity.Property(e => e.SemiGrupa)
                    .HasColumnName("semi_grupa")
                    .HasMaxLength(10);

                entity.HasOne(d => d.IdSpecializareNavigation)
                    .WithMany(p => p.Formatie)
                    .HasForeignKey(d => d.IdSpecializare)
                    .HasConstraintName("fk_specializare");
            });

            modelBuilder.Entity<Materie>(entity =>
            {
                entity.HasKey(e => e.IdMaterie)
                    .HasName("materie_pkey");

                entity.ToTable("materie");

                entity.Property(e => e.IdMaterie).HasColumnName("id_materie");

                entity.Property(e => e.Cod)
                    .HasColumnName("cod")
                    .HasMaxLength(20);

                entity.Property(e => e.Descriere)
                    .HasColumnName("descriere")
                    .HasMaxLength(100);

                entity.Property(e => e.Finalizare)
                    .HasColumnName("finalizare")
                    .HasMaxLength(2);

                entity.Property(e => e.NrCredite).HasColumnName("nr_credite");

                entity.Property(e => e.NrPachet).HasColumnName("nr_pachet");

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(50);

                entity.Property(e => e.TipActivitate).HasColumnName("tip_activitate");
            });

            modelBuilder.Entity<MaterieSpecializare>(entity =>
            {
                entity.HasKey(e => new { e.IdSpecializare, e.IdMaterie })
                    .HasName("pk_ms");

                entity.ToTable("materie_specializare");

                entity.Property(e => e.IdSpecializare).HasColumnName("id_specializare");

                entity.Property(e => e.IdMaterie).HasColumnName("id_materie");

                entity.Property(e => e.Semestru).HasColumnName("semestru");

                entity.HasOne(d => d.IdMaterieNavigation)
                    .WithMany(p => p.MaterieSpecializare)
                    .HasForeignKey(d => d.IdMaterie)
                    .HasConstraintName("fk_materie");

                entity.HasOne(d => d.IdSpecializareNavigation)
                    .WithMany(p => p.MaterieSpecializare)
                    .HasForeignKey(d => d.IdSpecializare)
                    .HasConstraintName("fk_specializare");
            });

            modelBuilder.Entity<Orarmaterie>(entity =>
            {
                entity.HasKey(e => new { e.OraInceput, e.OraSfarsit, e.IdMaterie, e.IdProfesor, e.ZiuaSaptamanii, e.Frecventa, e.IdFormatie, e.IdSpecializare })
                    .HasName("pk_orar_materie");

                entity.ToTable("orarmaterie");

                entity.Property(e => e.OraInceput)
                    .HasColumnName("ora_inceput")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.OraSfarsit)
                    .HasColumnName("ora_sfarsit")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.IdMaterie).HasColumnName("id_materie");

                entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");

                entity.Property(e => e.ZiuaSaptamanii)
                    .HasColumnName("ziua_saptamanii")
                    .HasMaxLength(20);

                entity.Property(e => e.Frecventa)
                    .HasColumnName("frecventa")
                    .HasMaxLength(10);

                entity.Property(e => e.IdFormatie).HasColumnName("id_formatie");

                entity.Property(e => e.IdSpecializare).HasColumnName("id_specializare");

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("date");

                entity.Property(e => e.IdSala).HasColumnName("id_sala");

                entity.Property(e => e.Tip)
                    .HasColumnName("tip")
                    .HasMaxLength(10);

                entity.HasOne(d => d.IdMaterieNavigation)
                    .WithMany(p => p.Orarmaterie)
                    .HasForeignKey(d => d.IdMaterie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_materie");

                entity.HasOne(d => d.IdProfesorNavigation)
                    .WithMany(p => p.Orarmaterie)
                    .HasForeignKey(d => d.IdProfesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_profesor");

                entity.HasOne(d => d.IdSalaNavigation)
                    .WithMany(p => p.Orarmaterie)
                    .HasForeignKey(d => d.IdSala)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_sala");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Orarmaterie)
                    .HasForeignKey(d => new { d.IdFormatie, d.IdSpecializare })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_formatie");
            });

            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("profesor_pkey");

                entity.ToTable("profesor");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasDefaultValueSql("nextval('users_id_user_seq'::regclass)");

                entity.Property(e => e.Cnp)
                    .HasColumnName("cnp")
                    .HasMaxLength(13);

                entity.Property(e => e.Grad)
                    .HasColumnName("grad")
                    .HasMaxLength(15);

                entity.Property(e => e.IdDepartament).HasColumnName("id_departament");

                entity.Property(e => e.Mail)
                    .HasColumnName("mail")
                    .HasMaxLength(50);

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(50);

                entity.Property(e => e.PHash).HasColumnName("p_hash");

                entity.Property(e => e.PSalt).HasColumnName("p_salt");

                entity.Property(e => e.Prenume)
                    .HasColumnName("prenume")
                    .HasMaxLength(50);

                entity.Property(e => e.Site)
                    .HasColumnName("site")
                    .HasMaxLength(50);

                entity.Property(e => e.TipUtilizator)
                    .HasColumnName("tip_utilizator")
                    .HasMaxLength(15);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdDepartamentNavigation)
                    .WithMany(p => p.Profesor)
                    .HasForeignKey(d => d.IdDepartament)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_departament");
            });

            modelBuilder.Entity<Regulament>(entity =>
            {
                entity.HasKey(e => e.IdRegulament)
                    .HasName("regulament_pkey");

                entity.ToTable("regulament");

                entity.Property(e => e.IdRegulament).HasColumnName("id_regulament");

                entity.Property(e => e.Continut)
                    .HasColumnName("continut")
                    .HasMaxLength(500);

                entity.Property(e => e.IdFacultate).HasColumnName("id_facultate");

                entity.Property(e => e.Titlu)
                    .HasColumnName("titlu")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdFacultateNavigation)
                    .WithMany(p => p.Regulament)
                    .HasForeignKey(d => d.IdFacultate)
                    .HasConstraintName("regulament_id_facultate_fkey");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => new { e.IdProfesor, e.IdMaterie, e.IdCriteriu, e.IdStudent, e.AnDeStudiu, e.AnCaledaristic })
                    .HasName("pk_review");

                entity.ToTable("review");

                entity.Property(e => e.IdProfesor).HasColumnName("id_profesor");

                entity.Property(e => e.IdMaterie).HasColumnName("id_materie");

                entity.Property(e => e.IdCriteriu).HasColumnName("id_criteriu");

                entity.Property(e => e.IdStudent).HasColumnName("id_student");

                entity.Property(e => e.AnDeStudiu).HasColumnName("an_de_studiu");

                entity.Property(e => e.AnCaledaristic)
                    .HasColumnName("an_caledaristic")
                    .HasMaxLength(50);

                entity.Property(e => e.Nota).HasColumnName("nota");

                entity.HasOne(d => d.IdCriteriuNavigation)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.IdCriteriu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_criteriu");

                entity.HasOne(d => d.IdProfesorNavigation)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.IdProfesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_profesor");

                entity.HasOne(d => d.Detaliucontract)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => new { d.IdMaterie, d.IdStudent, d.AnDeStudiu, d.AnCaledaristic })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_detaliu_contract");
            });

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.IdSala)
                    .HasName("sala_pkey");

                entity.ToTable("sala");

                entity.Property(e => e.IdSala).HasColumnName("id_sala");

                entity.Property(e => e.Locatie)
                    .HasColumnName("locatie")
                    .HasMaxLength(50);

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Specializare>(entity =>
            {
                entity.HasKey(e => e.IdSpecializare)
                    .HasName("specializare_pkey");

                entity.ToTable("specializare");

                entity.Property(e => e.IdSpecializare).HasColumnName("id_specializare");

                entity.Property(e => e.Cod)
                    .HasColumnName("cod")
                    .HasMaxLength(50);

                entity.Property(e => e.IdFacultate).HasColumnName("id_facultate");

                entity.Property(e => e.Nivel)
                    .HasColumnName("nivel")
                    .HasMaxLength(8);

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdFacultateNavigation)
                    .WithMany(p => p.Specializare)
                    .HasForeignKey(d => d.IdFacultate)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_facultate");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("student_pkey");

                entity.ToTable("student");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasDefaultValueSql("nextval('users_id_user_seq'::regclass)");

                entity.Property(e => e.Cnp)
                    .HasColumnName("cnp")
                    .HasMaxLength(13);

                entity.Property(e => e.Cup)
                    .HasColumnName("cup")
                    .HasMaxLength(10);

                entity.Property(e => e.IdFormatie).HasColumnName("id_formatie");

                entity.Property(e => e.IdSpecializare).HasColumnName("id_specializare");

                entity.Property(e => e.Mail)
                    .HasColumnName("mail")
                    .HasMaxLength(50);

                entity.Property(e => e.NrMatricol)
                    .HasColumnName("nr_matricol")
                    .HasMaxLength(10);

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(50);

                entity.Property(e => e.PHash).HasColumnName("p_hash");

                entity.Property(e => e.PSalt).HasColumnName("p_salt");

                entity.Property(e => e.Prenume)
                    .HasColumnName("prenume")
                    .HasMaxLength(50);

                entity.Property(e => e.TipUtilizator)
                    .HasColumnName("tip_utilizator")
                    .HasMaxLength(15);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => new { d.IdFormatie, d.IdSpecializare })
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_formatie");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("users_pkey");

                entity.ToTable("users");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Cnp)
                    .HasColumnName("cnp")
                    .HasMaxLength(13);

                entity.Property(e => e.Mail)
                    .HasColumnName("mail")
                    .HasMaxLength(50);

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(50);

                entity.Property(e => e.PHash).HasColumnName("p_hash");

                entity.Property(e => e.PSalt).HasColumnName("p_salt");

                entity.Property(e => e.Prenume)
                    .HasColumnName("prenume")
                    .HasMaxLength(50);

                entity.Property(e => e.TipUtilizator)
                    .HasColumnName("tip_utilizator")
                    .HasMaxLength(15);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
