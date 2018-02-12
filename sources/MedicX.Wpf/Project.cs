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
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.\n See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.\n If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Wpf
{
    internal class Project
    {
        private readonly Random random;

        public ObservableCollection<MedicalEvent> Events { get; }

        public Project()
        {
            random = new Random();
            List<MedicalEvent> events = CreateEvents();
            Events = new ObservableCollection<MedicalEvent>(events);
        }

        private static List<MedicalEvent> CreateEvents()
        {
            return new List<MedicalEvent>
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
                    Comments = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.\nSed vehicula dolor et facilisis ullamcorper.\nIn eu faucibus lacus.\nSed et purus convallis, pulvinar neque eget, ultricies quam.\nVivamus faucibus metus ligula, non sagittis turpis elementum at.\nSuspendisse porta egestas felis quis facilisis.\nSed euismod vitae augue vel placerat.\nVivamus mollis nibh quis lectus imperdiet, ut maximus velit tempor.\nNullam eros mauris, ullamcorper vel ultrices et, tincidunt viverra elit.\nMauris tristique sem eu justo malesuada maximus id a nibh."
                },
                new Investigation
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
                    Comments = "Morbi et neque quis mi tristique feugiat eget vel sem.\nDonec justo diam, pharetra ut maximus in, convallis eget felis.\nAliquam tempus nisl quis imperdiet mattis.\nAliquam erat volutpat.\nNulla suscipit tellus vitae mi sollicitudin volutpat.\nDonec dapibus facilisis quam in bibendum.\nProin id suscipit urna.\nNulla sed rhoncus neque.\nNunc pharetra in nisi sed blandit."
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
                    Comments = "Maecenas in augue congue, iaculis elit a, varius nisl.\nClass aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.\nVestibulum tempus tellus eget erat ornare ullamcorper.\nDonec a sollicitudin libero, in elementum magna.\nDonec interdum risus dolor, eget ultricies sapien posuere et.\nLorem ipsum dolor sit amet, consectetur adipiscing elit.\nNulla justo tortor, porta eu nulla pellentesque, condimentum interdum sapien."
                }
            };
        }

        public void RemoveRandomEvent()
        {
            if (Events.Count == 0)
                return;

            int index = random.Next(Events.Count);
            Events.RemoveAt(index);
        }

        public void ReplaceRandomEvent()
        {
            if (Events.Count == 0)
                return;

            int index = random.Next(Events.Count);
            Events[index] = new MedicalEvent();
        }

        public void ClearAllEvents()
        {
            Events.Clear();
        }

        public void MoveRandomEvent()
        {
            if (Events.Count <= 1)
                return;

            int index1 = random.Next(Events.Count);
            int index2 = random.Next(Events.Count);

            if (index1 == index2)
            {
                if (index1 == Events.Count - 1)
                    index2--;
                else
                    index2++;
            }

            Events.Move(index1, index2);
        }
    }
}