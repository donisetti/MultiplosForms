using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.MultiplosForms.Domain
{
    public interface IClienteService
    {
        void Cadastrar(Cliente novoCliente);
        IEnumerable<Cliente> Pesquisar(string nome);
    }
}
