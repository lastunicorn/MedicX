using System;
using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.MedicX.TableDisplay
{
    public class Table
    {
        private const HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Left;

        /// <summary>
        /// Gets or sets the title of the current instance of the <see cref="Table"/>.
        /// </summary>
        public MultilineText Title { get; set; }

        /// <summary>
        /// Gets or sets the padding applyed to the left side of every cell.
        /// </summary>
        public int PaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the padding applyed to the right side of every cell.
        /// </summary>
        public int PaddingRight { get; set; }

        /// <summary>
        /// Gets or sets the padding applyed to the right and left side of every cell.
        /// If the left padding is different from the right padding, -1 is returned.
        /// </summary>
        public int Padding
        {
            get { return PaddingLeft == PaddingRight ? PaddingLeft : -1; }
            set { PaddingLeft = PaddingRight = value; }
        }

        /// <summary>
        /// Gets a value that specifies if border lines should be drawn between rows.
        /// </summary>
        public bool DrawLinesBetweenRows { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment for the content of the cells contained by the current table.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Gets the list of columns contained by the current table.
        /// </summary>
        public List<Column> Columns { get; } = new List<Column>();

        /// <summary>
        /// The list of rows contained by the current table.
        /// </summary>
        private readonly List<Row> rows = new List<Row>();

        /// <summary>
        /// Gets the number of rows contained by the current instance of the <see cref="Table"/>.
        /// </summary>
        public int RowCount => rows.Count;

        /// <summary>
        /// Gets the number of columns contained by the current instance of the <see cref="Table"/>.
        /// </summary>
        public int ColumnCount => Columns.Count;

        /// <summary>
        /// Gets or sets the minimum width of the table.
        /// </summary>
        public int MinWidth { get; set; }

        public bool DisplayColumnHeaders { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class.
        /// </summary>
        public Table()
            : this(MultilineText.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class with
        /// the table title.
        /// </summary>
        public Table(string title)
            : this(new MultilineText(title))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class with
        /// the table title.
        /// </summary>
        public Table(MultilineText title)
        {
            Title = title ?? MultilineText.Empty;

            HorizontalAlignment = DefaultHorizontalAlignment;
            DrawLinesBetweenRows = false;
            DisplayColumnHeaders = false;
            PaddingLeft = 1;
            PaddingRight = 1;
        }

        /// <summary>
        /// Gets the row at the specified index.
        /// </summary>
        /// <param name="rowIndex">The zero-based index of the row to get.</param>
        /// <returns>The row at the specified index.</returns>
        public Row this[int rowIndex] => rows[rowIndex];

        /// <summary>
        /// Gets the cell at the specified location.
        /// </summary>
        /// <param name="rowIndex">The zero-based row index of the cell to get.</param>
        /// <param name="columnIndex">The zero-based column index of the cell to get.</param>
        /// <returns>The cell at the specified location.</returns>
        public Cell this[int rowIndex, int columnIndex] => rows[rowIndex][columnIndex];

        /// <summary>
        /// Adds a new row to the current table.
        /// </summary>
        /// <param name="row">The row to be added.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddRow(Row row)
        {
            if (row == null) throw new ArgumentNullException(nameof(row));

            rows.Add(row);
        }

        /// <summary>
        /// Adds a new row to the current table.
        /// </summary>
        /// <param name="cells">The list of cells of the new row.</param>
        public void AddRow(Cell[] cells)
        {
            if (cells == null) throw new ArgumentNullException(nameof(cells));

            rows.Add(new Row(cells));
        }

        /// <summary>
        /// Adds a new row to the current table.
        /// </summary>
        /// <param name="texts">The list of cell contents of the new row.</param>
        public void AddRow(string[] texts)
        {
            if (texts == null) throw new ArgumentNullException(nameof(texts));

            Row row = new Row();

            foreach (string text in texts)
                row.Cells.Add(new Cell(text));

            rows.Add(row);
        }

        /// <summary>
        /// Calculates and returns the dimensions of the current instance of the <see cref="Table"/> displayed in text mode.
        /// </summary>
        /// <returns>The dimensions of the current instance of the <see cref="Table"/> displayed in text mode.</returns>
        private TableDimensions CalculateTableDimensions()
        {
            TableDimensions dimensions = new TableDimensions
            {
                Width = MinWidth > 0 ? MinWidth : 0
            };

            int longRowWidth = 0;

            // Calculate table title row width.

            int titleRowWidth = 1 + PaddingLeft + PaddingRight + 1;
            if (Title != null)
            {
                titleRowWidth += Title.Size.Width;
            }
            if (dimensions.Width < titleRowWidth) dimensions.Width = titleRowWidth;

            // Calculate the header dimensions.

            if (DisplayColumnHeaders)
            {
                int headerRowWidth = 1; // The table left border
                int headerRowHeight = 0;

                for (int i = 0; i < Columns.Count; i++)
                {
                    Column column = Columns[i];

                    dimensions.ColumnsWidth.Add(0);

                    int cellWidth = 0;
                    int cellHeight = 0;

                    if (column.Title != null)
                    {
                        cellWidth = PaddingLeft + column.Title.Size.Width + PaddingRight;
                        cellHeight = column.Title.Size.Height;
                    }
                    else
                    {
                        cellWidth = PaddingLeft + PaddingRight;
                    }

                    if (dimensions.ColumnsWidth[i] < cellWidth)
                    {
                        dimensions.ColumnsWidth[i] = cellWidth;
                        headerRowWidth += cellWidth + 1; // The cell width + cell right border
                    }
                    else
                    {
                        headerRowWidth += dimensions.ColumnsWidth[i] + 1; // The cell width + cell right border
                    }

                    if (headerRowHeight < cellHeight)
                    {
                        headerRowHeight = cellHeight;
                    }
                }

                dimensions.HeaderHeight = headerRowHeight;

                if (longRowWidth < headerRowWidth) longRowWidth = headerRowWidth;
            }

            //

            for (int i = 0; i < rows.Count; i++)
            {
                Row row = rows[i];

                int rowWidth = 1; // The table left border
                int rowHeight = 0;

                for (int j = 0; j < row.Cells.Count; j++)
                {
                    Cell cell = row.Cells[j];

                    if (j == dimensions.ColumnsWidth.Count)
                    {
                        dimensions.ColumnsWidth.Add(0);
                    }

                    int cellWidth = 0;
                    int cellHeight = 0;

                    if (cell.Content != null)
                    {
                        cellWidth = PaddingLeft + cell.Content.Size.Width + PaddingRight;
                        cellHeight = cell.Content.Size.Height;
                    }
                    else
                    {
                        cellWidth = PaddingLeft + PaddingRight;
                    }

                    if (dimensions.ColumnsWidth[j] < cellWidth)
                    {
                        dimensions.ColumnsWidth[j] = cellWidth;
                        rowWidth += cellWidth + 1; // The cell width + cell right border
                    }
                    else
                    {
                        rowWidth += dimensions.ColumnsWidth[j] + 1; // The cell width + cell right border
                    }

                    if (rowHeight < cellHeight)
                    {
                        rowHeight = cellHeight;
                    }
                }

                dimensions.RowsHeight.Add(rowHeight);

                if (longRowWidth < rowWidth) longRowWidth = rowWidth;
            }

            if (dimensions.Width < longRowWidth)
            {
                dimensions.Width = longRowWidth;
            }
            else if (dimensions.Width > longRowWidth)
            {
                int diff = dimensions.Width - longRowWidth;
                int colCount = dimensions.ColumnsWidth.Count;

                for (int i = 0; i < diff; i++)
                {
                    dimensions.ColumnsWidth[i % colCount]++;
                }
            }

            return dimensions;
        }

        /// <summary>
        /// Returns the string representation of the current instance.
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            TableDimensions dimensions = CalculateTableDimensions();
            string rowSeparator = GetHorizontalRowBorder(dimensions);

            DrawTableTitle(sb, dimensions, rowSeparator);
            DrawColumnHeaders(sb, dimensions, rowSeparator);
            DrawRows(sb, dimensions, rowSeparator);

            return sb.ToString();
        }

        private void DrawTableTitle(StringBuilder sb, TableDimensions dimensions, string rowSeparator)
        {
            if (Title.Size.Height > 0)
            {
                sb.AppendLine("+" + string.Empty.PadRight(dimensions.Width - 2, '-') + "+");

                for (int i = 0; i < Title.Size.Height; i++)
                    sb.AppendLine("| " + Title.Lines[i].PadRight(dimensions.Width - 4, ' ') + " |");
            }

            sb.AppendLine(rowSeparator);
        }

        private void DrawColumnHeaders(StringBuilder sb, TableDimensions dimensions, string rowSeparator)
        {
            if (!DisplayColumnHeaders || dimensions.HeaderHeight == 0)
                return;

            for (int k = 0; k < dimensions.HeaderHeight; k++)
            {
                sb.Append("|");

                for (int i = 0; i < Columns.Count; i++)
                {
                    Column column = Columns[i];

                    if (k < column.Title.Size.Height)
                    {
                        string leftPadding = string.Empty.PadRight(PaddingLeft, ' ');
                        string rightPadding = string.Empty.PadRight(PaddingRight, ' ');
                        int cellInnerWidth = dimensions.ColumnsWidth[i] - PaddingLeft - PaddingRight;
                        string content;

                        HorizontalAlignment alignment = ColumnHeaderHorizontalAlignment(i);
                        switch (alignment)
                        {
                            default:
                            case HorizontalAlignment.Default:
                            case HorizontalAlignment.Left:
                                content = column.Title.Lines[k].PadRight(cellInnerWidth, ' ');
                                break;

                            case HorizontalAlignment.Right:
                                content = column.Title.Lines[k].PadLeft(cellInnerWidth, ' ');
                                break;

                            case HorizontalAlignment.Center:
                                int totalSpaces = cellInnerWidth - column.Title.Size.Width;
                                int rightSpaces = (int)Math.Ceiling((double)totalSpaces / 2);
                                content = column.Title.Lines[k].PadLeft(cellInnerWidth - rightSpaces, ' ').PadRight(cellInnerWidth, ' ');
                                break;
                        }

                        sb.Append(leftPadding + content + rightPadding + "|");
                    }
                    else
                    {
                        sb.Append(" " + string.Empty.PadRight(dimensions.ColumnsWidth[i] - 2, ' ') + " |");
                    }
                }

                sb.AppendLine();
            }

            sb.AppendLine(rowSeparator);
        }

        private void DrawRows(StringBuilder sb, TableDimensions dimensions, string rowSeparator)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                Row row = rows[i];

                if (i > 0) sb.AppendLine();

                for (int k = 0; k < dimensions.RowsHeight[i]; k++)
                {
                    if (k > 0) sb.AppendLine();

                    sb.Append("|");
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        Cell cell = row.Cells[j];
                        if (k < cell.Content.Size.Height)
                        {
                            string leftPadding = string.Empty.PadRight(PaddingLeft, ' ');
                            string rightPadding = string.Empty.PadRight(PaddingRight, ' ');
                            int cellInnerWidth = dimensions.ColumnsWidth[j] - PaddingLeft - PaddingRight;
                            string content;

                            HorizontalAlignment alignment = CellHorizontalAlignment(i, j);
                            switch (alignment)
                            {
                                default:
                                case HorizontalAlignment.Default:
                                case HorizontalAlignment.Left:
                                    content = cell.Content.Lines[k].PadRight(cellInnerWidth, ' ');
                                    break;

                                case HorizontalAlignment.Right:
                                    content = cell.Content.Lines[k].PadLeft(cellInnerWidth, ' ');
                                    break;

                                case HorizontalAlignment.Center:
                                    int totalSpaces = cellInnerWidth - cell.Content.Size.Width;
                                    //int leftSpaces = (int)Math.Floor(totalSpaces / 2);
                                    int rightSpaces = (int)Math.Ceiling((double)totalSpaces / 2);
                                    content = cell.Content.Lines[k].PadLeft(cellInnerWidth - rightSpaces, ' ').PadRight(cellInnerWidth, ' ');
                                    break;
                            }

                            sb.Append(leftPadding + content + rightPadding + "|");
                        }
                        else
                        {
                            sb.Append(" " + string.Empty.PadRight(dimensions.ColumnsWidth[j] - 2, ' ') + " |");
                        }
                    }
                }

                if (DrawLinesBetweenRows)
                {
                    sb.AppendLine();
                    sb.Append(rowSeparator);
                }
            }

            if (!DrawLinesBetweenRows)
            {
                sb.AppendLine();
                sb.Append(rowSeparator);
            }
        }

        private HorizontalAlignment ColumnHeaderHorizontalAlignment(int columnIndex)
        {
            HorizontalAlignment alignment = HorizontalAlignment.Default;

            if (columnIndex < Columns.Count)
                alignment = Columns[columnIndex].HorizontalAlignment;

            if (alignment == HorizontalAlignment.Default)
            {
                alignment = HorizontalAlignment == HorizontalAlignment.Default
                    ? HorizontalAlignment.Left
                    : DefaultHorizontalAlignment;
            }

            return alignment;
        }

        /// <summary>
        /// Returns the horizontal alignment of a cell from the current instance of the <see cref="Table"/>.
        /// </summary>
        /// <param name="rowIndex">The zero-based row index of the cell to get.</param>
        /// <param name="columnIndex">The zero-based column index of the cell to get.</param>
        /// <returns>The horizontal alignment of the cell.</returns>
        private HorizontalAlignment CellHorizontalAlignment(int rowIndex, int columnIndex)
        {
            HorizontalAlignment alignment = rows[rowIndex][columnIndex].HorizontalAlignment;

            if (alignment == HorizontalAlignment.Default)
            {
                Column column = Columns.Count > columnIndex ? Columns[columnIndex] : null;
                if (column != null)
                {
                    alignment = column.HorizontalAlignment;
                }

                if (alignment == HorizontalAlignment.Default)
                {
                    alignment = HorizontalAlignment == HorizontalAlignment.Default
                        ? HorizontalAlignment.Left
                        : DefaultHorizontalAlignment;
                }
            }

            return alignment;
        }

        /// <summary>
        /// Returns the line border between two rows.
        /// </summary>
        /// <param name="tableDimensions">The <see cref="TableDimensions"/> instance used to create the border line.</param>
        /// <returns>The line border between two rows</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private string GetHorizontalRowBorder(TableDimensions tableDimensions)
        {
            if (tableDimensions == null) throw new ArgumentNullException(nameof(tableDimensions));

            StringBuilder value = new StringBuilder("+");

            foreach (int columnWidth in tableDimensions.ColumnsWidth)
            {
                string horizontalCellBorder = string.Empty.PadRight(columnWidth, '-');
                value.Append(horizontalCellBorder + "+");
            }

            return value.ToString();
        }
    }
}
