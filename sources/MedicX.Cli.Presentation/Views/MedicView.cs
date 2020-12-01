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

using System.Collections.Generic;
using DustInTheWind.ConsoleTools.InputControls;
using DustInTheWind.MedicX.CommandApplication.AddMedic;
using DustInTheWind.MedicX.Domain.Entities;

namespace MedicX.Cli.Presentation.Views
{
    public class MedicView : IMedicView
    {
        public PersonName ReadName()
        {
            TextInputControl textInputControl = new TextInputControl();

            string firstName = textInputControl.Read("First Name");
            string middleName = textInputControl.Read("Middle Name");
            string lastName = textInputControl.Read("Last Name");

            return new PersonName
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName
            };
        }

        public List<string> ReadSpecializations()
        {
            ListInputControl listInputControl = new ListInputControl();

            return listInputControl.Read("Specializations");
        }

        public string ReadComments()
        {
            TextInputControl textInputControl = new TextInputControl();
            return textInputControl.Read("Comments");
        }
    }
}