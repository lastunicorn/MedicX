﻿// MedicX
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

using System.Collections.Generic;
using System.Linq;
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Wpf
{
    public class MainViewModel
    {
        public List<EventViewModel> Events { get; }

        public MainViewModel()
        {
            Project project = new Project();

            Events = Convert(project.Events);
        }

        private static List<EventViewModel> Convert(IEnumerable<Event> projectEvents)
        {
            return projectEvents
                .Where(x => x != null)
                .Select(x =>
                {
                    switch (x)
                    {
                        case Consultation consultation:
                            return ToViewModel(consultation);

                        case InvestigationInstance investigationInstance:
                            return ToViewModel(investigationInstance);

                        default:
                            return null;
                    }
                })
                .Where(x => x != null)
                .ToList();
        }

        private static EventViewModel ToViewModel(Consultation consultation)
        {
            return new EventViewModel
            {
                Date = consultation.Date,
                Type = EventType.Consult,
                Medic = consultation.Medic?.Name?.ToString(),
                Description = consultation.Comments
            };
        }

        private static EventViewModel ToViewModel(InvestigationInstance investigationInstance)
        {
            return new EventViewModel
            {
                Date = investigationInstance.Date,
                Type = EventType.Investigation,
                Medic = investigationInstance.SentBy?.Name?.ToString(),
                Description = investigationInstance.Comments
            };
        }
    }
}