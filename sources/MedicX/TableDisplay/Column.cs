namespace DustInTheWind.MedicX.TableDisplay
{
    /// <summary>
    /// Represents a column in the <see cref="Table"/> class.
    /// </summary>
    public class Column
    {
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        public string Name { get; }

        public MultilineText Header { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment for the content of the cells represented by the current instance of the <see cref="Column"/>.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class with
        /// the a name.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        public Column(string name)
            : this(name, new MultilineText(name), HorizontalAlignment.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        public Column(string name, MultilineText header)
            : this(name, header, HorizontalAlignment.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class with
        /// the a name and the horizontal alignment applyed to the cells represented by the column.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        public Column(string name, HorizontalAlignment horizontalAlignment)
            : this(name, new MultilineText(name), horizontalAlignment)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        public Column(string name, MultilineText header, HorizontalAlignment horizontalAlignment)
        {
            Name = name;
            Header = header;
            HorizontalAlignment = horizontalAlignment;
        }
    }
}
