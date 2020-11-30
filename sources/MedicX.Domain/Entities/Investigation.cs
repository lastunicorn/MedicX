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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace DustInTheWind.MedicX.Domain.Entities
{
    public class Investigation : MedicalEvent, IEquatable<Investigation>
    {
        private Medic sentBy;
        private ObservableCollection<Test> tests;

        public Medic SentBy
        {
            get => sentBy;
            set
            {
                sentBy = value;

                OnSentByChanged();
                OnChanged();
            }
        }

        public ObservableCollection<Test> Tests
        {
            get => tests;
            set
            {
                if (tests != null)
                    tests.CollectionChanged -= HandleTestsCollectionChanged;

                tests = value;

                if (tests != null)
                    tests.CollectionChanged += HandleTestsCollectionChanged;

                OnChanged();
            }
        }

        public event EventHandler SentByChanged;

        private void HandleTestsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Test investigationTest in e.NewItems)
                        investigationTest.Changed += HandleInvestigationResultChanged;
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (Test investigationTest in e.OldItems)
                        investigationTest.Changed -= HandleInvestigationResultChanged;
                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (Test investigationTest in e.OldItems)
                        investigationTest.Changed -= HandleInvestigationResultChanged;
                    foreach (Test investigationTest in e.NewItems)
                        investigationTest.Changed += HandleInvestigationResultChanged;
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    foreach (Test investigationTest in e.OldItems)
                        investigationTest.Changed -= HandleInvestigationResultChanged;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            OnChanged();
        }

        private void HandleInvestigationResultChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        public void CopyFrom(Investigation investigation)
        {
            base.CopyFrom(investigation);

            SentBy = investigation.SentBy;
            Tests = investigation.Tests == null
                ? null
                : new ObservableCollection<Test>(investigation.Tests);
        }

        public override void CopyFrom(MedicalEvent medicalEvent)
        {
            if (medicalEvent is Investigation investigation)
                CopyFrom(investigation);
            else
                base.CopyFrom(medicalEvent);
        }

        public override bool Contains(string text)
        {
            return base.Contains(text) ||
                   (SentBy != null && SentBy.Contains(text)) ||
                   (Tests != null && Tests.Any(x => x != null && x.Contains(text)));
        }

        public bool Equals(Investigation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return base.Equals(other) && Equals(sentBy, other.sentBy);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((Investigation)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (sentBy != null ? sentBy.GetHashCode() : 0);
            }
        }

        protected virtual void OnSentByChanged()
        {
            SentByChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}