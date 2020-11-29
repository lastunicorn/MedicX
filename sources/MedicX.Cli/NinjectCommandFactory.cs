using System;
using MedicX.Cli.Presentation;
using Ninject;

namespace DustInTheWind.MedicX.Cli.Bootstrapper
{
    internal class NinjectCommandFactory : ICommandFactory
    {
        private readonly IKernel kernel;

        public NinjectCommandFactory(IKernel kernel)
        {
            this.kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        }

        public ICommand Create(Type commandType)
        {
            if (!typeof(ICommand).IsAssignableFrom(commandType))
                throw new ArgumentException("The specified type is not a command.", nameof(commandType));

            return (ICommand)kernel.Get(commandType);
        }
    }
}