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
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Wpf
{
    internal class Project
    {
        public List<Event> Events { get; }

        public Project()
        {
            Events = CreateEvents();
        }

        private static List<Event> CreateEvents()
        {
            List<Event> events = new List<Event>
            {
                new Consultation
                {
                    Date = DateTime.Today,
                    Medic = new Medic
                    {
                        Name = new PersonName
                        {
                            FirstName = "Gigi",
                            LastName = "Castravete"
                        }
                    },
                    Comments = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vehicula dolor et facilisis ullamcorper. In eu faucibus lacus. Sed et purus convallis, pulvinar neque eget, ultricies quam. Vivamus faucibus metus ligula, non sagittis turpis elementum at. Suspendisse porta egestas felis quis facilisis. Sed euismod vitae augue vel placerat. Vivamus mollis nibh quis lectus imperdiet, ut maximus velit tempor. Nullam eros mauris, ullamcorper vel ultrices et, tincidunt viverra elit. Mauris tristique sem eu justo malesuada maximus id a nibh."
                },
                new InvestigationInstance
                {
                    Date = DateTime.Today,
                    SentBy = new Medic
                    {
                        Name = new PersonName
                        {
                            FirstName = "Gigi",
                            LastName = "Castravete"
                        }
                    },
                    Comments = "Morbi et neque quis mi tristique feugiat eget vel sem. Donec justo diam, pharetra ut maximus in, convallis eget felis. Aliquam tempus nisl quis imperdiet mattis. Aliquam erat volutpat. Nulla suscipit tellus vitae mi sollicitudin volutpat. Donec dapibus facilisis quam in bibendum. Proin id suscipit urna. Nulla sed rhoncus neque. Nunc pharetra in nisi sed blandit."
                },
                new Consultation
                {
                    Date = DateTime.Today,
                    Medic = new Medic
                    {
                        Name = new PersonName
                        {
                            FirstName = "Gigi",
                            LastName = "Castravete"
                        }
                    },
                    Comments = "Maecenas in augue congue, iaculis elit a, varius nisl. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Vestibulum tempus tellus eget erat ornare ullamcorper. Donec a sollicitudin libero, in elementum magna. Donec interdum risus dolor, eget ultricies sapien posuere et. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla justo tortor, porta eu nulla pellentesque, condimentum interdum sapien."
                }
            };
            return events;
        }
    }
}