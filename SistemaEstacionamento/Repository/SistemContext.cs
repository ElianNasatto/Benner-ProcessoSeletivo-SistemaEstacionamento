using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SistemContext : DbContext
    {
        public SistemContext() : base("SqlServerConnection")
        {

        }

        public DbSet<Preco> Precos { get; set; }

        public DbSet<Carro> Carros { get; set; }

        public DbSet<Estacionado> Estacionados { get; set; }
    }
}
