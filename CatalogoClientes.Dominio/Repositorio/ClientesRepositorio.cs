using CatalogoClientes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoClientes.Dominio.Repositorio
{
    public class ClientesRepositorio : IRepositorio<Cliente>
    {
        private ClienteContexto contexto;

        public ClientesRepositorio(ClienteContexto clienteContexto)
        {
            this.contexto = clienteContexto;
        }
        public IEnumerable<Cliente> GetTodos()
        {
            return contexto.Clientes.ToList();
        }
    }
}
