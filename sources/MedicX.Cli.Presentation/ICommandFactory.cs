using System;

namespace MedicX.Cli.Presentation
{
    public interface ICommandFactory
    {
        ICommand Create(Type commandType);
    }
}