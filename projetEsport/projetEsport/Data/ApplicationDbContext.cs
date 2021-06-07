using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projetEsport.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace projetEsport.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Licencie> Licencies { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Jeu> Jeux { get; set; }
        public DbSet<TypeCompetition> TypesDeCompetition { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<InvitationEquipe> InvitationsEquipes { get; set; }
        public DbSet<TypeMatche> TypesDeMatche { get; set; }
        public DbSet<Matche> Matches { get; set; }
        public DbSet<CompetitionEquipe> CompetitionEquipe{ get; set; }
        public DbSet<EquipeMatche> EquipeMatche { get; set; }
    }
}
