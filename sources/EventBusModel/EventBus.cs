using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBusModel
{
    public class EventBus
    {
        private readonly List<Event> events = new List<Event>();

        public Event this[string eventName]
        {
            get
            {
                Event ev = events.FirstOrDefault(x => x.Name == eventName);

                if (ev == null)
                {
                    ev = new Event(eventName);
                    events.Add(ev);
                }

                return ev;
            }
        }

        public void Subscribe(string eventName, Delegate action)
        {
            Event ev = events.FirstOrDefault(x => x.Name == eventName);

            if (ev == null)
            {
                ev = new Event(eventName);
                events.Add(ev);
            }

            ev.Subscribe(action);
        }

        public void Raise(string eventName, params object[] eventData)
        {
            Event ev = events.FirstOrDefault(x => x.Name == eventName);
            ev?.Raise(eventData);
        }
    }
}