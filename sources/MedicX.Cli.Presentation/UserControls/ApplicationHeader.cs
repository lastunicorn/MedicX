using System;
using System.Reflection;
using DustInTheWind.ConsoleTools;

namespace MedicX.Cli.Presentation.UserControls
{
    internal class ApplicationHeader
    {
        public void Display()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyName assemblyName = assembly.GetName();
            Version version = assemblyName.Version;
            string versionAsString = version.ToString(3);

            CustomConsole.WriteLine("MedicX " + versionAsString);
            CustomConsole.WriteLine("===============================================================================");
            CustomConsole.WriteLine();
        }
    }
}