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
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.MedicX.Domain.Entities
{
    public class Test
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Substance { get; set; }
        
        public string Method { get; set; }
        
        public List<TestItem> Items { get; set; }
        
        public string Comments { get; set; }

        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public bool Contains(string text)
        {
            return (Name != null && Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (Substance != null && Substance.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (Method != null && Method.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                   (Items != null && Items.Any(x => x != null && x.Contains(text))) ||
                   (Comments != null && Comments.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}