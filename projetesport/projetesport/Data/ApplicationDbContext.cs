using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using projetesport.Models;

namespace projetesport.Data
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
    }
}
