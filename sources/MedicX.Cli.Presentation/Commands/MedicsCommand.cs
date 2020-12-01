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
using System.Collections.ObjectModel;
using System.Linq;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.MedicX.Domain;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;
using MedicX.Cli.Presentation.Views;

namespace MedicX.Cli.Presentation.Commands
{
    [Command(Names = "medic, medics")]
    internal class MedicsCommand : ICommand
    {
        private readonly ProjectRepository projectRepository;
        private readonly DisplayMedicsView view;

        public MedicsCommand(ProjectRepository projectRepository, DisplayMedicsView view)
        {
            this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            this.view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Execute(UserCommand command)
        {
            if (command.Parameters.Count > 0)
            {
                if (command.Parameters.Count > 0 && command.Parameters.ElementAt(0).Name?.ToLower() == "add")
                {
                    AddMedic();
                }
                else
                {
                    string searchText = command.Parameters.ElementAt(0).Name;
                    SearchMedic(searchText);
                }
            }
            else
            {
                DisplayAllMedics();
            }
        }

        private void AddMedic()
        {
            projectRepository.RunWithUnitOfWork(unitOfWork =>
            {
                Medic medic = ReadMedic();
                unitOfWork.MedicRepository.Add(medic);
                unitOfWork.Save();
            });
        }

        private static Medic ReadMedic()
        {
            TextInputControl textInputControl = new TextInputControl();
            ListInputControl listInputControl = new ListInputControl();

            string firstName = textInputControl.Read("First Name");
            string middleName = textInputControl.Read("Middle Name");
            string lastName = textInputControl.Read("Last Name");

            List<string> specializations = listInputControl.Read("Specializations");

            string comments = textInputControl.Read("Comments");

            return new Medic
            {
                Id = Guid.NewGuid(),
                Name = new PersonName
                {
                    FirstName = firstName,
                    MiddleName = middleName,
                    LastName = lastName
                },
                Specializations = new ObservableCollection<string>(specializations),
                Comments = comments
            };
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

        private void SearchMedic(string searchText)
        {
            projectRepository.RunWithUnitOfWork(unitOfWork =>
            {
                IMedicRepository medicRepository = unitOfWork.MedicRepository;

                List<Medic> medics = medicRepository.Search(searchText);
                view.DisplayMedics(medics);
            });
        }
    }
}