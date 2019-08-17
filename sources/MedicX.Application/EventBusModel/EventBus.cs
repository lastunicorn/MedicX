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

//using System;
//using System.Collections.Generic;

//namespace DustInTheWind.MedicX.Application.EventBusModel
//{
//    public class EventBus
//    {
//        private readonly object SubscriptionsLock = new object();
//        private readonly Dictionary<Type, List<Action>> subscriptions = new Dictionary<Type, List<Action>>();

//        public void Subscribe<TEventBase>(Action<TEventBase> action)
//        {
//            if (action == null)
//                throw new ArgumentNullException(nameof(action));

//            lock (SubscriptionsLock)
//            {
//                if (!subscriptions.ContainsKey(typeof(TEventBase)))
//                    subscriptions.Add(typeof(TEventBase), new List<Action>());

//                subscriptions[typeof(TEventBase)].Add(action);
//            }
//        }

//        public void Subscribea(Action<MyEventData> handle)
//        {
//        }
//    }
//}
