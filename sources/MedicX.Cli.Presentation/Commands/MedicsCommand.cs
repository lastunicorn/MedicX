﻿// MedicX
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
using DustInTheWind.ConsoleTools;
using DustInTheWind.MedicX.Domain;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;
using MedicX.Cli.Presentation.Views;

namespace MedicX.Cli.Presentation.Commands
{
    [Command(Names = "medic")]
    internal class MedicsCommand : ICommand
    {
        private readonly ProjectRepository projectRepository;
        private readonly DisplayMedicsView view;
        private readonly RequestBus requestBus;

        public MedicsCommand(ProjectRepository projectRepository, DisplayMedicsView view, RequestBus requestBus)
        {
            this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
        }

        public void Execute(UserCommand command)
        {
            DisplayAllMedics();
        }

        private void DisplayAllMedics()
        {
            projectRepository.RunWithUnitOfWork(unitOfWork =>
            {
                IMedicRepository medicRepository = unitOfWork.MedicRepository;

                List<Medic> medics = medicRepository.GetAll();
                view.DisplayMedics(medics);
            });
        }
    }
}