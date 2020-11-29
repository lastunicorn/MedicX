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
    public class Consultation : MedicalEvent, IEquatable<Consultation>
    {
        private ObservableCollection<Prescription> prescriptions;

        public string Reason { get; set; }

        public string Conclusions { get; set; }

        public ObservableCollection<Prescription> Prescriptions
        {
            get => prescriptions;
            set
            {
                if (prescriptions != null)
                    prescriptions.CollectionChanged -= HandlePrescriptionsCollectionChanged;

                prescriptions = value;

                if (prescriptions != null)
                    prescriptions.CollectionChanged += HandlePrescriptionsCollectionChanged;

                OnChanged();
            }
        }

        private void HandlePrescriptionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Prescription prescription in e.NewItems)
                        prescription.Changed += HandleClinicChanged;
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (Prescription prescription in e.OldItems)
                        prescription.Changed -= HandleClinicChanged;
                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (Prescription prescription in e.OldItems)
                        prescription.Changed -= HandleClinicChanged;
                    foreach (Prescription prescription in e.NewItems)
                        prescription.Changed += HandleClinicChanged;
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    foreach (Prescription prescription in e.OldItems)
                        prescription.Changed -= HandleClinicChanged;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            OnChanged();
        }

        private void HandleClinicChanged(object sender, EventArgs e)
        {
            OnChanged();
        }

        public void CopyFrom(Consultation consultation)
        {
            base.CopyFrom(consultation);

            Reason = consultation.Reason;
            Conclusions = consultation.Conclusions;

            Prescriptions = consultation.Prescriptions == null
                ? null
                : new ObservableCollection<Prescription>(consultation.Prescriptions);
        }

        public override void CopyFrom(MedicalEvent medicalEvent)
        {
            if (medicalEvent is Consultation consultation)
                CopyFrom(consultation);
            else
                base.CopyFrom(medicalEvent);
        }

        public override bool Contains(string text)
        {
            return base.Contains(text) ||
                   Prescriptions != null && Prescriptions.Any(x => x != null && x.Contains(text)) ||
                   Reason != null && Reason.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                   Conclusions != null && Conclusions.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public bool Equals(Consultation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return base.Equals(other) &&
                   ((prescriptions == null & other.prescriptions == null) || (prescriptions != null & other.prescriptions != null && prescriptions.SequenceEqual(other.prescriptions))) &&
                   Reason == other.Reason &&
                   Conclusions == other.Conclusions;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((Consultation)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (prescriptions != null ? prescriptions.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Reason != null ? Reason.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Conclusions != null ? Conclusions.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}