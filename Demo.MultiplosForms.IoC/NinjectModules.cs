using Demo.MultiplosForms.Domain;
using Ninject.Modules;

namespace Demo.MultiplosForms.IoC
{
    public class NinjectModules:NinjectModule
    {
        public override void Load()
        {
            Bind<IClienteService>().To<ClienteService>();
        }
    }
}
