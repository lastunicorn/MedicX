using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EventBusModel
{
    public class Event
    {
        public string Name { get; }
        private readonly List<Delegate> subscriptions = new List<Delegate>();

        public Event(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void Subscribe(Delegate action)
        {
            subscriptions.Add(action);
        }

        public void Raise(params object[] eventData)
        {
            foreach (Delegate subscription in subscriptions)
            {
                MethodInfo methodInfo = subscription.Method;

                int paramCount = methodInfo.GetParameters().Length;

                object[] newEventData = BuildEventData(eventData, paramCount);
                methodInfo.Invoke(subscription.Target, newEventData);
            }
        }

        private static object[] BuildEventData(object[] eventData, int neededCount)
        {
            if (eventData.Length < neededCount)
            {
                int missingParametersCount = neededCount - eventData.Length;
                IEnumerable<object> emptyParameters = Enumerable.Repeat(null as object, missingParametersCount);

                return eventData
                    .Concat(emptyParameters)
                    .ToArray();
            }

            if (eventData.Length > neededCount)
            {
                return eventData
                    .Take(neededCount)
                    .ToArray();
            }

            return eventData;
        }
    }
}