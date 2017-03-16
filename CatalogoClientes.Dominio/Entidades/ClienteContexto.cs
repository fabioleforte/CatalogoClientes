using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoClientes.Dominio.Entidades
{
    public class ClienteContexto:DbContext
    {
        public ClienteContexto(): base("name=ConexaoClientes") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ClienteContexto>(new CreateDatabaseIfNotExists<ClienteContexto>());
        }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
