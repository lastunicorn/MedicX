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

using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.ConsoleTools;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;
using MedicX.Cli.Presentation.Views;

namespace MedicX.Cli.Presentation.Commands
{
    internal class DisplayMedicsCommand : ICommand
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly DisplayMedicsView view;

        public DisplayMedicsCommand(IUnitOfWork unitOfWork, DisplayMedicsView view)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public bool IsMatch(UserCommand command)
        {
            return (command.Name == "medic" || command.Name == "medics") &&
                   (command.Parameters.Count == 0 || command.Parameters.ElementAt(0).Name?.ToLower() != "add");
        }

        public void Execute(UserCommand command)
        {
            if (command.Parameters.Count > 0)
            {
                string searchText = command.Parameters.ElementAt(0).Name;
                SearchMedic(searchText);
            }
            else
            {
                DisplayAllMedics();
            }
        }

        private void DisplayAllMedics()
        {
            IMedicRepository medicRepository = unitOfWork.MedicRepository;

            List<Medic> medics = medicRepository.GetAll();
            view.DisplayMedics(medics);
        }

        private void SearchMedic(string searchText)
        {
            IMedicRepository medicRepository = unitOfWork.MedicRepository;

            List<Medic> medics = medicRepository.Search(searchText);
            view.DisplayMedics(medics);
        }
    }
}