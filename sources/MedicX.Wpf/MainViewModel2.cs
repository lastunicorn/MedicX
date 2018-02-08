using System.Collections.Generic;
using System.Linq;
using DustInTheWind.MedicX.Common.Entities;

namespace DustInTheWind.MedicX.Wpf
{
    public class MainViewModel2
    {
        public List<EventViewModel> Events { get; }

        public MainViewModel2()
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