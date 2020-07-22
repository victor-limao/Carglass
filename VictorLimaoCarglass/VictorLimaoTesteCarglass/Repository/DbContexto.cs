using VictorLimaoTesteCarglass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Web;

namespace EstruturarRepository.Repository
{
    public class DbContexto : DbContext 
    {
        public DbSet<Empresa>  empresa { get; set; }
        public DbSet<Fornecedor>  fornecedor { get; set; }
        public DbSet<FornecedorPF>  fornecedorpf { get; set; }
        public DbContexto() : base("conn")
        {
            Database.SetInitializer<DbContexto>(new NullDatabaseInitializer<DbContexto>());
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false; 
        }
    }
}