using Infraestructure.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Infraestructure.Core.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<ProductEntity> ProductEntity { get; set; }
        public DbSet<InvoiceEntity> InvoiceEntity { get; set; }
        public DbSet<InvoiceDetailEntity> InvoiceDetailEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>()
               .HasIndex(b => b.Code)
               .IsUnique();

            //modelBuilder.Entity<TypeStateEntity>().Property(t => t.IdTypeState).ValueGeneratedNever();          
        }
    }
}
