using System;
using System.Collections.Generic;
using System.Linq;
using Demo.MultiplosForms.Domain;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace Demo.MultiplosForms.Tests
{
    public class Dado_um_cliente : SpecBase
    {
        protected ClienteService clienteService;
        protected Cliente novoCliente;

        protected override void Establish_context()
        {
            base.Establish_context();
            clienteService = new ClienteService();
        }
    }

    public class Quando_for_cadastrar_um_cliente : Dado_um_cliente
    {
        protected override void Establish_context()
        {
            base.Establish_context();
            novoCliente = new Cliente()
            {
                Nome = "João",
                SobreNome = "Francisco",
                DataNascimento = DateTime.Parse("16/04/1991")
            };
        }


        protected override void Because_of()
        {
            base.Because_of();
            clienteService.Cadastrar(novoCliente);
        }

        [Test]
        public void Adicionar_o_cliente_na_listagem()
        {
            Cliente.ClientesCadastrados.ShouldContain(novoCliente);
        }

    }

    public class Quando_for_pesquisar_por_um_cliente_existente : Dado_um_cliente
    {
        private List<Cliente> clientesCadastrados;
        private IEnumerable<Cliente> _result;

        protected override void Establish_context()
        {
            base.Establish_context();
            clientesCadastrados = new List<Cliente>()
            {
                new Cliente{DataNascimento = DateTime.Parse("1/02/1985"),Id = new Guid(),Nome = "Alberto",SobreNome = "Silva"},
                new Cliente{DataNascimento = DateTime.Parse("1/02/1980"),Id = new Guid(),Nome = "Maria",SobreNome = "Ferreira"},
                new Cliente{DataNascimento = DateTime.Parse("1/02/1974"),Id = new Guid(),Nome = "Pedro",SobreNome = "De camargo"},
                new Cliente{DataNascimento = DateTime.Parse("1/02/1994"),Id = new Guid(),Nome = "Pedro",SobreNome = "Ferraz"},
            };

            Cliente.ClientesCadastrados = clientesCadastrados;
        }

        protected override void Because_of()
        {
            base.Because_of();
            _result = clienteService.Pesquisar("Pedro");
        }

        [Test]
        public void Retornar_cliente()
        {
            _result.ShouldNotBeNull();
            _result.Count().ShouldBeGreaterThanOrEqualTo(2);
        }
    }
}