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
using System.Windows;
using DustInTheWind.MedicX.Application.ExitApplication;

namespace MedicX.Wpf.UI.Areas.Main.Commands
{
    public class SaveConfirmationQuestion : ISaveConfirmationQuestion
    {
        public ConfirmationResponse Ask()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to save the project before closing?", "Save", MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    return ConfirmationResponse.Yes;

                case MessageBoxResult.No:
                    return ConfirmationResponse.No;

                case MessageBoxResult.Cancel:
                    return ConfirmationResponse.Cancel;

                default:
                    throw new Exception("Invalid answer received when asking for saving the project.");
            }
        }
    }
}