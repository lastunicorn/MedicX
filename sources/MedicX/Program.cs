// MedicX
// Copyright (C) 2017 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Reflection;
using System.Threading;
using DustInTheWind.MedicX.Flows;
using DustInTheWind.MedicX.Persistence.Json;
using DustInTheWind.MedicX.Utils;

namespace DustInTheWind.MedicX
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                DisplayAppHeader();

                //Dictionary<string, Type> flows = new Dictionary<string, Type>
                //{
                //    { "medic", typeof(MedicsFlow) },
                //    { "consult", typeof(ConsultationsFlow) },
                //    { "consultation", typeof(ConsultationsFlow) }
                //};

                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    bool exitRequested = false;

                    while (!exitRequested)
                    {
                        Console.WriteLine();
                        Console.Write("> ");
                        string command = Console.ReadLine();

                        IFlow flow = null;

                        switch (command)
                        {
                            case "medic":
                                CustomConsole.WriteLine();
                                flow = new MedicsFlow(unitOfWork);
                                break;

                            case "consult":
                            case "consultation":
                                CustomConsole.WriteLine();
                                flow = new ConsultationsFlow(unitOfWork);
                                break;

                            case "save":
                                flow = new SaveFlow(unitOfWork);
                                break;

                            case "exit":
                            case "quit":
                                exitRequested = true;
                                CustomConsole.WriteLine();
                                CustomConsole.WriteLine("Bye!");
                                break;

                            case "help":
                                CustomConsole.WriteLine();
                                CustomConsole.WriteEmphasies("Commands: ");
                                CustomConsole.WriteLine("medic, consultation, save, exit, help");
                                break;

                            default:
                                CustomConsole.WriteLineError("Unknown command");
                                break;
                        }

                        flow?.Run();
                    }
                }
            }
            catch (Exception ex)
            {
                CustomConsole.WriteError(ex);
                CustomConsole.Pause();
            }

            Thread.Sleep(300);
        }

        private static void DisplayAppHeader()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            AssemblyName assemblyName = assembly.GetName();
            Version version = assemblyName.Version;
            string versionAsString = version.ToString(3);

            CustomConsole.WriteLine("MedicX " + versionAsString);
        }
    }

    //internal class Command
    //{
    //    public string Name { get; set; }
    //    public List<CommandParameter> Parameters { get; set; }
    //}

    //internal class CommandParameter
    //{
    //    public string Name { get; set; }
    //    public string Value { get; set; }
    //}

    //internal class MedicCommand
    //{
    //    private readonly UnitOfWork unitOfWork;

    //    public string Name => "medic";

    //    public MedicCommand(UnitOfWork unitOfWork)
    //    {
    //        if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
    //        this.unitOfWork = unitOfWork;
    //    }

    //    public bool TryRun(string command)
    //    {
    //        string[] elements = command.Split(' ');

    //        if(elements.Length== 0 || elements[0] != "medic")
    //            return false;

    //        MedicsFlow flow = new MedicsFlow(unitOfWork);
    //        flow.Run();

    //        return true;
    //    }
    //}
}