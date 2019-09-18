// MedicX
// Copyright (C) 2017-2018 Dust in the Wind
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

using System.Diagnostics;
using System.Reflection;
using DustInTheWind.MedicX.Domain.Entities;

namespace MedicX.Wpf.UI.Areas.Main.ViewModels
{
    public class MainWindowTitle
    {
        public string Value { get; }

        public MainWindowTitle()
            : this(ProjectStatus.None)
        {
        }

        private MainWindowTitle(string value)
        {
            Value = value;
        }

        public MainWindowTitle(ProjectStatus projectStatus)
        {
            string value = BuildName();

            Value = projectStatus == ProjectStatus.Saved || projectStatus == ProjectStatus.None
                ? value
                : value + " *";
        }

        private static string BuildName()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            AssemblyInformationalVersionAttribute attribute = assembly.GetCustomAttribute(typeof(AssemblyInformationalVersionAttribute)) as AssemblyInformationalVersionAttribute;

            string version;
            if (attribute == null)
            {
                AssemblyName assemblyName = assembly.GetName();

                version = assemblyName.Version.Build == 0
                    ? assemblyName.Version.ToString(2)
                    : assemblyName.Version.ToString(3);
            }
            else
            {
                version = attribute.InformationalVersion;
            }

            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            string productName = fileVersionInfo.ProductName;

            return $"{productName} {version}";
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(MainWindowTitle title)
        {
            return title.Value;
        }

        public static implicit operator MainWindowTitle(string title)
        {
            return new MainWindowTitle(title);
        }
    }
}