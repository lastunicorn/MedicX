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
using System.Threading.Tasks;
using DustInTheWind.MedicX.Domain.Entities;
using DustInTheWind.MedicX.RequestBusModel;

namespace DustInTheWind.MedicX.Application.ExitApplication
{
    internal class ExitApplicationRequestHandler : IRequestHandler<ExitApplicationRequest, bool>
    {
        private readonly MedicXApplication medicXApplication;
        private readonly ISaveConfirmationQuestion saveConfirmationQuestion;

        public ExitApplicationRequestHandler(MedicXApplication medicXApplication, ISaveConfirmationQuestion saveConfirmationQuestion)
        {
            this.medicXApplication = medicXApplication ?? throw new ArgumentNullException(nameof(medicXApplication));
            this.saveConfirmationQuestion = saveConfirmationQuestion ?? throw new ArgumentNullException(nameof(saveConfirmationQuestion));
        }

        public Task<bool> Handle(ExitApplicationRequest request)
        {
            bool allowToContinue = EnsureSave();

            if (allowToContinue)
                medicXApplication.UnloadProject();

            return Task.FromResult(allowToContinue);
        }

        private bool EnsureSave()
        {
            if (medicXApplication.CurrentProject.Status == ProjectStatus.Saved)
                return true;

            ConfirmationResponse response = saveConfirmationQuestion.Ask();

            switch (response)
            {
                case ConfirmationResponse.Yes:
                    medicXApplication.SaveCurrentProject();
                    return true;

                case ConfirmationResponse.No:
                    return true;

                case ConfirmationResponse.Cancel:
                    return false;

                default:
                    throw new Exception("Invalid answer received when asking for saving the project.");
            }
        }
    }
}