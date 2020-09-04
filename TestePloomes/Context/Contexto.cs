using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestePloomes.Models;

namespace TestePloomes.Context
{
    public class Contexto : DbContext
    {

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:testeploomesjames.database.windows.net,1433;Initial Catalog=TestePloomes;Persist Security Info=False;User ID=testeploomes;
                Password=T3stePloomes;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
    }
}
