using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace DustInTheWind.MedicX.Domain.Entities
{
    public class Record : IEquatable<Record>
    {
        private DateTime date;
        private ObservableCollection<string> labels;
        private string comments;

        public event EventHandler DateChanged;
        public event EventHandler Changed;

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnDateChanged();
                OnChanged();
            }
        }

        public ObservableCollection<string> Labels
        {
            get => labels;
            set
            {
                if (labels != null)
                    labels.CollectionChanged -= HandleLabelsCollectionChanged;

                labels = value;

                if (labels != null)
                    labels.CollectionChanged += HandleLabelsCollectionChanged;

                OnChanged();
            }
        }

        public string Comments
        {
            get => comments;
            set
            {
                comments = value;
                OnChanged();
            }
        }

        private void HandleLabelsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnChanged();
        }

        public virtual void CopyFrom(MedicalEvent medicalEvent)
        {
            Date = medicalEvent.Date;
            Labels = new ObservableCollection<string>(medicalEvent.Labels);
            Comments = medicalEvent.Comments;
        }

        public bool Equals(Record other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return date.Equals(other.date) &&
                   Equals(labels, other.labels) &&
                   comments == other.comments;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Record)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = date.GetHashCode();
                hashCode = (hashCode * 397) ^ (labels != null ? labels.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (comments != null ? comments.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnDateChanged()
        {
            DateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}