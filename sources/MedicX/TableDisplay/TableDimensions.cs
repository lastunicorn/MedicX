using System.Collections.Generic;

namespace DustInTheWind.MedicX.TableDisplay
{
    /// <summary>
    /// Represents the dimensions of a table displayed in text mode.
    /// </summary>
    internal class TableDimensions
    {
        /// <summary>
        /// Gets or sets the width of the table.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the header.
        /// </summary>
        public int HeaderHeight { get; set; }

        /// <summary>
        /// Gets a list containing the widths of the columns.
        /// </summary>
        public List<int> ColumnsWidth { get; } = new List<int>();

        /// <summary>
        /// Gets a list containing the heights of the rows.
        /// </summary>
        public List<int> RowsHeight { get; } = new List<int>();
    }
}