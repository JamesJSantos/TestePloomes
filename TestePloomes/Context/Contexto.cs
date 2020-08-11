using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestePloomes.Models;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace TestePloomes.Context
{
    public class Contexto : DbContext
    {

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TestePloomes;Trusted_Connection=True;");
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
    }
}
