using System.Web.Mvc;
using Demo.MultiplosForms.Domain;
using Demo.MultiplosForms.IoC;
using Ninject;

namespace Demo.MultiplosForms.Controllers
{

    public class HomeController : Controller
    {
        private IKernel kernel;

        public HomeController()
        {
            kernel = new StandardKernel(new NinjectModules());
        }

        public ActionResult Index()
        {
            ViewBag.Clientes = Cliente.ClientesCadastrados;
            return View();
        }


        public ActionResult Create(Cliente novoCliente)
        {
            if (Request.IsAjaxRequest())
            {
                kernel.Get<IClienteService>().Cadastrar(novoCliente);
                return PartialView("_Clientes", Cliente.ClientesCadastrados);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Pesquisar(string uiTxtNome)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Clientes", kernel.Get<IClienteService>().Pesquisar(uiTxtNome));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}