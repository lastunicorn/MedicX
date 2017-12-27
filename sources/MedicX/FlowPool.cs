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
using System.Linq;
using DustInTheWind.ConsoleTools;
using DustInTheWind.MedicX.Flows;
using DustInTheWind.MedicX.Persistence.Json;

namespace DustInTheWind.MedicX
{
    internal class FlowPool
    {
        private readonly UnitOfWork unitOfWork;
        private readonly MedicXApplication medicXApplication;

        public FlowPool(UnitOfWork unitOfWork, MedicXApplication medicXApplication)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
            if (medicXApplication == null) throw new ArgumentNullException(nameof(medicXApplication));

            this.unitOfWork = unitOfWork;
            this.medicXApplication = medicXApplication;
        }

        public IFlow Get(UserCommand command)
        {
            switch (command.Name)
            {
                case "medic":
                case "medics":
                    if (command.Parameters.Count > 0)
                    {
                        if (command.Parameters.ElementAt(0).Name?.ToLower() == "add")
                            return new AddMedicFlow(unitOfWork);

                        string searchText = command.Parameters.ElementAt(0).Name;
                        return new DisplayMedicsFlow(unitOfWork, searchText);
                    }
                    else
                    {
                        return new DisplayMedicsFlow(unitOfWork);
                    }

                case "clinic":
                case "clinics":
                    if (command.Parameters.Count > 0)
                    {
                        string searchText = command.Parameters.ElementAt(0).Name;
                        return new DisplayClinicsFlow(unitOfWork, searchText);
                    }
                    else
                    {
                        return new DisplayClinicsFlow(unitOfWork);
                    }

                case "consult":
                case "consults":
                case "consultation":
                case "consultations":
                    if (command.Parameters.Count > 0)
                    {
                        string searchText = command.Parameters.ElementAt(0).Name;
                        return new ConsultationsFlow(unitOfWork, searchText);
                    }
                    else
                    {
                        return new ConsultationsFlow(unitOfWork);
                    }

                case "save":
                    return new SaveFlow(unitOfWork);

                case "exit":
                case "quit":
                    return new ExitFlow(medicXApplication);

                case "help":
                    return new HelpFlow();

                default:
                    return null;
            }
        }
    }
}