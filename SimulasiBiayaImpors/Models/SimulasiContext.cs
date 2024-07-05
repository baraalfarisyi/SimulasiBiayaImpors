using Microsoft.EntityFrameworkCore;
using SimulasiBiayaImpors.Models;
using System.Collections.Generic;
namespace SimulasiBiayaImpor.SimulasiContext
{
    public class SimulasiContext : DbContext
    {
        public SimulasiContext(DbContextOptions<SimulasiContext> options) : base(options) { }

        public DbSet<BiayaImpor> BiayaImpors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-O597ERE\\SQLEXPRESS;Database=SimulasiDB;Trusted_Connection=True;");
            }
        }
    }
}
