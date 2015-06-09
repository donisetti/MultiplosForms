using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.MultiplosForms.Domain
{
    public class ClienteService : IClienteService
    {
        public void Cadastrar(Cliente novoCliente)
        {

            
            novoCliente.Id = Guid.NewGuid();
            Cliente.ClientesCadastrados.Add(novoCliente);

        }


        public IEnumerable<Cliente> Pesquisar(string nome)
        {
            return Cliente.ClientesCadastrados.Where(cliente => cliente.Nome.Contains(nome));
        }
    }
}
