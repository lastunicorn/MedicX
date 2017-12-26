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
using DustInTheWind.ConsoleTools;
using DustInTheWind.MedicX.Persistence.Json;

namespace DustInTheWind.MedicX
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                DisplayAppHeader();
                
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    MedicXApplication medicXApplication = new MedicXApplication(unitOfWork);
                    medicXApplication.Run();
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
            CustomConsole.WriteLine("===============================================================================");
        }
    }
}