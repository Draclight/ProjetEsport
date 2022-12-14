// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using projetEsport.Data;

namespace projetEsport.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210609211328_AddVainqueurMatchEquipe")]
    partial class AddVainqueurMatchEquipe
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("projetEsport.Models.Competition", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreeLe")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateFin")
                        .HasColumnType("datetime2");

                    b.Property<int>("JeuID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifieeLe")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProprietaireID")
                        .HasColumnType("int");

                    b.Property<int>("TypeCompetitionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("JeuID");

                    b.HasIndex("ProprietaireID");

                    b.HasIndex("TypeCompetitionID");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("projetEsport.Models.CompetitionEquipe", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompetitionID")
                        .HasColumnType("int");

                    b.Property<bool>("EncoreEnCompetition")
                        .HasColumnType("bit");

                    b.Property<int>("EquipeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CompetitionID");

                    b.HasIndex("EquipeID");

                    b.ToTable("CompetitionEquipe");
                });

            modelBuilder.Entity("projetEsport.Models.Equipe", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreeLe")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<int>("JeuID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifieeLe")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("JeuID");

                    b.ToTable("Equipes");
                });

            modelBuilder.Entity("projetEsport.Models.EquipeMatche", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EquipesDisputesID")
                        .HasColumnType("int");

                    b.Property<int>("MatchesDisputesID")
                        .HasColumnType("int");

                    b.Property<bool>("Vainqueur")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("EquipesDisputesID");

                    b.HasIndex("MatchesDisputesID");

                    b.ToTable("EquipeMatche");
                });

            modelBuilder.Entity("projetEsport.Models.InvitationEquipe", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAccepter")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateEnvoi")
                        .HasColumnType("datetime2");

                    b.Property<int>("EquipeID")
                        .HasColumnType("int");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<int>("LicencieID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EquipeID");

                    b.HasIndex("LicencieID");

                    b.ToTable("InvitationsEquipes");
                });

            modelBuilder.Entity("projetEsport.Models.Jeu", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreeLe")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifieeLe")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Jeux");
                });

            modelBuilder.Entity("projetEsport.Models.Licencie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CreateurEquipe")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreeLe")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EquipeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifieeLe")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pseudo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UtilisateurID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("EquipeID");

                    b.HasIndex("UtilisateurID");

                    b.ToTable("Licencies");
                });

            modelBuilder.Entity("projetEsport.Models.Matche", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompetitionID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreeLe")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateMatche")
                        .HasColumnType("datetime2");

                    b.Property<int?>("JeuID")
                        .HasColumnType("int");

                    b.Property<bool>("MatcheTeminer")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifieeLe")
                        .HasColumnType("datetime2");

                    b.Property<int>("TypeMatcheID")
                        .HasColumnType("int");

                    b.Property<int>("VictoireEquipeA")
                        .HasColumnType("int");

                    b.Property<int>("VictoireEquipeB")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CompetitionID");

                    b.HasIndex("JeuID");

                    b.HasIndex("TypeMatcheID");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("projetEsport.Models.TypeCompetition", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreeLe")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifieeLe")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TypeCompetitionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("TypeCompetitionID");

                    b.ToTable("TypesDeCompetition");
                });

            modelBuilder.Entity("projetEsport.Models.TypeMatche", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreeLe")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifieeLe")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TypesDeMatche");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("projetEsport.Models.Competition", b =>
                {
                    b.HasOne("projetEsport.Models.Jeu", "Jeu")
                        .WithMany("Competitions")
                        .HasForeignKey("JeuID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projetEsport.Models.Licencie", "Proprietaire")
                        .WithMany("CompetitionsCrees")
                        .HasForeignKey("ProprietaireID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projetEsport.Models.TypeCompetition", "TypeCompetition")
                        .WithMany()
                        .HasForeignKey("TypeCompetitionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Jeu");

                    b.Navigation("Proprietaire");

                    b.Navigation("TypeCompetition");
                });

            modelBuilder.Entity("projetEsport.Models.CompetitionEquipe", b =>
                {
                    b.HasOne("projetEsport.Models.Competition", "Competition")
                        .WithMany("EquipesDeLaCompetition")
                        .HasForeignKey("CompetitionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projetEsport.Models.Equipe", "Equipe")
                        .WithMany()
                        .HasForeignKey("EquipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");

                    b.Navigation("Equipe");
                });

            modelBuilder.Entity("projetEsport.Models.Equipe", b =>
                {
                    b.HasOne("projetEsport.Models.Jeu", "Jeu")
                        .WithMany("EquipesCreePourJeu")
                        .HasForeignKey("JeuID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Jeu");
                });

            modelBuilder.Entity("projetEsport.Models.EquipeMatche", b =>
                {
                    b.HasOne("projetEsport.Models.Equipe", "EquipesDisputes")
                        .WithMany("MatchesDisputes")
                        .HasForeignKey("EquipesDisputesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projetEsport.Models.Matche", "MatchesDisputes")
                        .WithMany("EquipesDisputes")
                        .HasForeignKey("MatchesDisputesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EquipesDisputes");

                    b.Navigation("MatchesDisputes");
                });

            modelBuilder.Entity("projetEsport.Models.InvitationEquipe", b =>
                {
                    b.HasOne("projetEsport.Models.Equipe", "Equipe")
                        .WithMany()
                        .HasForeignKey("EquipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projetEsport.Models.Licencie", "Licencie")
                        .WithMany("InvitationEquipe")
                        .HasForeignKey("LicencieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipe");

                    b.Navigation("Licencie");
                });

            modelBuilder.Entity("projetEsport.Models.Licencie", b =>
                {
                    b.HasOne("projetEsport.Models.Equipe", "Equipe")
                        .WithMany("Membres")
                        .HasForeignKey("EquipeID");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Utilisateur")
                        .WithMany()
                        .HasForeignKey("UtilisateurID");

                    b.Navigation("Equipe");

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("projetEsport.Models.Matche", b =>
                {
                    b.HasOne("projetEsport.Models.Competition", "Competition")
                        .WithMany("MatchesDisputes")
                        .HasForeignKey("CompetitionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("projetEsport.Models.Jeu", null)
                        .WithMany("MatchesDisputes")
                        .HasForeignKey("JeuID");

                    b.HasOne("projetEsport.Models.TypeMatche", "TypeMatche")
                        .WithMany("Matches")
                        .HasForeignKey("TypeMatcheID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competition");

                    b.Navigation("TypeMatche");
                });

            modelBuilder.Entity("projetEsport.Models.TypeCompetition", b =>
                {
                    b.HasOne("projetEsport.Models.TypeCompetition", null)
                        .WithMany("Competitions")
                        .HasForeignKey("TypeCompetitionID");
                });

            modelBuilder.Entity("projetEsport.Models.Competition", b =>
                {
                    b.Navigation("EquipesDeLaCompetition");

                    b.Navigation("MatchesDisputes");
                });

            modelBuilder.Entity("projetEsport.Models.Equipe", b =>
                {
                    b.Navigation("MatchesDisputes");

                    b.Navigation("Membres");
                });

            modelBuilder.Entity("projetEsport.Models.Jeu", b =>
                {
                    b.Navigation("Competitions");

                    b.Navigation("EquipesCreePourJeu");

                    b.Navigation("MatchesDisputes");
                });

            modelBuilder.Entity("projetEsport.Models.Licencie", b =>
                {
                    b.Navigation("CompetitionsCrees");

                    b.Navigation("InvitationEquipe");
                });

            modelBuilder.Entity("projetEsport.Models.Matche", b =>
                {
                    b.Navigation("EquipesDisputes");
                });

            modelBuilder.Entity("projetEsport.Models.TypeCompetition", b =>
                {
                    b.Navigation("Competitions");
                });

            modelBuilder.Entity("projetEsport.Models.TypeMatche", b =>
                {
                    b.Navigation("Matches");
                });
#pragma warning restore 612, 618
        }
    }
}
