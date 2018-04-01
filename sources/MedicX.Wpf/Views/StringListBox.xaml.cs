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

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DustInTheWind.MedicX.Wpf.Views
{
    /// <summary>
    /// Interaction logic for StringListBox.xaml
    /// </summary>
    public partial class StringListBox : UserControl
    {
        public StringListBox()
        {
            InitializeComponent();
            Box.DataContext = this;
            TitleLabel.DataContext = this;
        }

        private void AddClicked(object sender, RoutedEventArgs e)
        {
            ItemsSource.Add(AddStringBox.Text);
            AddStringBox.Clear();
            object o = DataContext;
            if (!(o is INotifyCollectionChanged))
            {
                DataContext = null;
                DataContext = o;
            }
        }

        private void MenuDeleteClicked(object sender, RoutedEventArgs e)
        {
            ItemsSource.Remove(Box.SelectedItem.ToString());
        }

        private void OnDeletePressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete) MenuDeleteClicked(sender, e);
        }

        private void OnEnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) AddClicked(this, e);
        }

        public ICollection<string> ItemsSource
        {
            get => (ICollection<string>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ICollection<string>), typeof(StringListBox), null);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("TitleProperty", typeof(string), typeof(StringListBox), null);
    }
}