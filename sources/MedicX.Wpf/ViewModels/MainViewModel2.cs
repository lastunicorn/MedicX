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

using DustInTheWind.MedicX.Wpf.Commands;

namespace DustInTheWind.MedicX.Wpf.ViewModels
{
    internal class MainViewModel2 : ViewModelBase
    {
        public EventsViewModel Events { get; } = new EventsViewModel();

        public AddCommand AddCommand { get; }
        public RemoveCommand RemoveCommand { get; }
        public ReplaceCommand ReplaceCommand { get; }
        public ClearCommand ClearCommand { get; }
        public MoveCommand MoveCommand { get; }

        public MainViewModel2()
        {
            Project project = new Project();

            AddCommand = new AddCommand(project);
            RemoveCommand = new RemoveCommand(project);
            ReplaceCommand = new ReplaceCommand(project);
            ClearCommand = new ClearCommand(project);
            MoveCommand = new MoveCommand(project);

            Events.SourceEvents = project.Events;
        }
    }
}