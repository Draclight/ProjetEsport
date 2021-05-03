﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public DbSet<Licencie> Licencie { get; set; }
        public DbSet<Equipe> Equipe { get; set; }
        public DbSet<Jeu> Jeu { get; set; }
        public DbSet<TypeCompetition> TypeCompetition { get; set; }
        public DbSet<Competition> Competition { get; set; }
        public DbSet<ClassementCompetition> ClassementCompetition { get; set; }
        public DbSet<CompetitionEquipe> CompetitionEquipe { get; set; }
        public DbSet<CompetitionJeu> CompetitionJeu { get; set; }
        public DbSet<InvitationEquipe> InvitationEquipe { get; set; }
    }
}
