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

namespace DustInTheWind.MedicX.Domain.Entities
{
    public class TestItem
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string MeasurementUnit { get; set; }
        
        public float? MinValue { get; set; }
        
        public float? MaxValue { get; set; }
        
        public float? Value { get; set; }

        public string Comments { get; set; }

        public bool Contains(string text)
        {
            return (Name != null && Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (MeasurementUnit != null && MeasurementUnit.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (MinValue != null && MinValue.ToString().IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (MaxValue != null && MaxValue.ToString().IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (Value != null && Value.ToString().IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (Comments != null && Comments.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}