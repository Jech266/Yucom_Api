using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yucom.Entity;

namespace Yucom
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventoPresentador>()
                .HasKey(al => new {al.EventoId, al.PresentadorId});
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Presentador> Presentadors { get; set; }
        public DbSet<Establecimiento> Establecimientos {get; set; }
        public DbSet<Evento> Eventos {get; set; }
        public DbSet<Asiento> Asientos {get; set; }
        public DbSet<Boleto> Boletos {get; set; }
        public DbSet<Costo> Costos {get; set; }
        public DbSet<Reservacion> Reservaciones {get; set; }
        public DbSet<EventoPresentador> EventoPresentadors {get; set; }
        
    }
}