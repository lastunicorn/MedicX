using System;
using System.Collections.Generic;

namespace DustInTheWind.MedicX.TableDisplay
{
    /// <summary>
    /// Represents a row in the <see cref="Table"/> class.
    /// </summary>
    public class Row
    {
        /// <summary>
        /// Gets the list of cells contained by the row.
        /// </summary>
        public List<Cell> Cells { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Row"/> class with default values.
        /// </summary>
        public Row()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Row"/> class with
        /// the list of cells.
        /// </summary>
        /// <param name="cells">The list of cells that will be contained by the new row.</param>
        public Row(Cell[] cells)
        {
            Cells = new List<Cell>();

            if (cells != null)
                Cells.AddRange(cells);
        }

        /// <summary>
        /// Gets or sets the cell at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the cell to get or set.</param>
        /// <returns>The cell at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Cell this[int index]
        {
            get { return Cells[index]; }
            set { Cells[index] = value; }
        }
    }
}
