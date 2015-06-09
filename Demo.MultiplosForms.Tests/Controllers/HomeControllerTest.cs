using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Demo.MultiplosForms.Controllers;
using Demo.MultiplosForms.Domain;
using Moq;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using System.Web;
using System.Net;
using System.Web.Routing;


namespace Demo.MultiplosForms.Tests.Controllers
{

    public class Dado_uma_HomeController : SpecBase
    {
        protected HomeController _controller;
        protected Cliente novoCliente;
        protected Mock<ControllerContext> ControllerContextMock;
        protected Mock<IClienteService> _ClienteServiceMock;

        protected override void Establish_context()
        {
            base.Establish_context();
            _controller = new HomeController();
            _ClienteServiceMock = new Mock<IClienteService>();
            ControllerContextMock = new Mock<ControllerContext>();
        }


    }

    public class Quando_for_realizar_uma_requisicao_para_cadastro_de_cliente : Dado_uma_HomeController
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

            _ClienteServiceMock.Setup(x => x.Cadastrar(novoCliente));
            ControllerContextMock.Setup(_ => _.HttpContext.Request["X-Requested-With"])
                .Returns("XMLHttpRequest");
        }

        protected override void Because_of()
        {
            base.Because_of();

            _controller.ControllerContext = ControllerContextMock.Object;
            _controller.Create(novoCliente);

        }

        [Test]
        public void Chamar_metodo_de_cadastro_do_ClienteService()
        {

            novoCliente.Id.ShouldNotBeNull();
        }
    }



    public class Quando_for_visualizar_a_Home : Dado_uma_HomeController
    {
        private ActionResult _actionResult;
        private ViewResult _ViewResult;


        protected override void Establish_context()
        {
            base.Establish_context();
            Cliente.ClientesCadastrados = new List<Cliente>(){
            (new Cliente { Nome = "Teste", SobreNome = "Test" })};
        }

        protected override void Because_of()
        {
            base.Because_of();
            _actionResult = _controller.Index();
        }

        [Test]
        public void Listar_todos_clientes_cadastrados()
        {

            _ViewResult = _actionResult as ViewResult;

            Extensions.ShouldBeInstanceOfType(_ViewResult.ViewBag.Clientes, typeof(IEnumerable<Cliente>));
            Extensions.ShouldNotBeNull(_ViewResult.ViewBag.Clientes);
        }

        public override void MainTeardown()
        {
            base.MainTeardown();
            Cliente.ClientesCadastrados.Clear();
        }
    }

    public class Quando_for_Pesquisar_um_cliente : Dado_uma_HomeController
    {
        private ActionResult _actionResult;
        private PartialViewResult _PartialViewResult;
        private string expected = "Pe";

        protected override void Establish_context()
        {
            base.Establish_context();

            _ClienteServiceMock.Setup(x => x.Pesquisar(expected)).Returns(new List<Cliente>());
            ControllerContextMock.Setup(_ => _.HttpContext.Request["X-Requested-With"])
                .Returns("XMLHttpRequest");

            Cliente.ClientesCadastrados = new List<Cliente> {
                new Cliente{DataNascimento = DateTime.Parse("1/02/1985"),Id = new Guid(),Nome = "Alberto",SobreNome = "Silva"},
                new Cliente{DataNascimento = DateTime.Parse("1/02/1980"),Id = new Guid(),Nome = "Maria",SobreNome = "Ferreira"},
                new Cliente{DataNascimento = DateTime.Parse("1/02/1974"),Id = new Guid(),Nome = "Pedro",SobreNome = "De camargo"},
                new Cliente{DataNascimento = DateTime.Parse("1/02/1994"),Id = new Guid(),Nome = "Pedro",SobreNome = "Ferraz"},
            };
        }

        protected override void Because_of()
        {
            base.Because_of();
            _controller.ControllerContext = ControllerContextMock.Object;
            _actionResult = _controller.Pesquisar(expected);
        }

        [Test]
        public void Listar_clientes_encontrados()
        {

            _PartialViewResult = _actionResult as PartialViewResult;

            Extensions.ShouldBeInstanceOfType(_PartialViewResult.Model, typeof(IEnumerable<Cliente>));
            Extensions.ShouldNotBeNull(_PartialViewResult.Model);
        }

    }
}
