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

namespace DustInTheWind.MedicX.Common.Entities
{
    public class Investigation : MedicalEvent
    {
        private Medic sentBy;
        private ObservableCollection<InvestigationResult> result;

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

        public ObservableCollection<InvestigationResult> Result
        {
            get => result;
            set
            {
                if (result != null)
                    result.CollectionChanged -= HandleResultCollectionChanged;

                result = value;

                if (result != null)
                    result.CollectionChanged += HandleResultCollectionChanged;

                OnChanged();
            }
        }

        public event EventHandler SentByChanged;

        private void HandleResultCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (InvestigationResult investigationResult in e.NewItems)
                        investigationResult.Changed += HandleInvestigationResultChanged;
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (InvestigationResult investigationResult in e.OldItems)
                        investigationResult.Changed -= HandleInvestigationResultChanged;
                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (InvestigationResult investigationResult in e.OldItems)
                        investigationResult.Changed -= HandleInvestigationResultChanged;
                    foreach (InvestigationResult investigationResult in e.NewItems)
                        investigationResult.Changed += HandleInvestigationResultChanged;
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    foreach (InvestigationResult investigationResult in e.OldItems)
                        investigationResult.Changed -= HandleInvestigationResultChanged;
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
            Result = investigation.Result == null
                ? null
                : new ObservableCollection<InvestigationResult>(investigation.Result);
        }

        public override void CopyFrom(MedicalEvent medicalEvent)
        {
            if (medicalEvent is Investigation investigation)
                CopyFrom(investigation);
            else
                base.CopyFrom(medicalEvent);
        }

        protected virtual void OnSentByChanged()
        {
            SentByChanged?.Invoke(this, EventArgs.Empty);
        }

        public override bool Contains(string text)
        {
            return base.Contains(text) ||
                   (SentBy != null && SentBy.Contains(text)) ||
                   (Result != null && Result.Any(x => x != null && x.Contains(text)));
        }
    }
}