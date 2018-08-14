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

using System.Windows;
using System.Windows.Controls;

namespace DustInTheWind.MedicX.Wpf.CustomControls
{
    public class BigListItem : Control
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(object), typeof(BigListItem));

        public object Text
        {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(object), typeof(BigListItem));

        public object Description
        {
            get => GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        static BigListItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BigListItem), new FrameworkPropertyMetadata(typeof(BigListItem)));
        }
    }
}