using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Persistence;
using MedicX.Cli.Presentation;
using Ninject;

namespace DustInTheWind.MedicX.Cli
{
    internal class Bootstrapper
    {
        public void Run()
        {
            IKernel kernel = new StandardKernel();

            kernel
                .Bind<IUnitOfWork>()
                .ToMethod(context => new UnitOfWork("medicx.zmdx"))
                .InSingletonScope();

            MedicXApplication application = kernel.Get<MedicXApplication>();
            application.Run();
        }
    }
}