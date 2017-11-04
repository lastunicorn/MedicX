namespace DustInTheWind.MedicX.TableDisplay
{
    /// <summary>
    ///     Represents a cell that contains a text.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Gets or sets the content of the cell.
        /// </summary>
        public MultilineText Content { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment of the content displayed in the cell.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell" /> class with
        /// the text contained by it.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        public Cell(string text)
            : this(new MultilineText(text), HorizontalAlignment.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell" /> class with
        /// the text contained by it and its horizontal alignment.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
        public Cell(string text, HorizontalAlignment horizontalAlignment)
            : this(new MultilineText(text), horizontalAlignment)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell" /> class with
        /// the text contained by it.
        /// </summary>
        /// <param name="text">The text displayed in the cell.</param>
        public Cell(MultilineText text)
            : this(text, HorizontalAlignment.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell" /> class with
        /// the text contained by it and its horizontal alignment.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="horizontalAlignment">The horizontal alignment of the content of the new cell.</param>
        public Cell(MultilineText text, HorizontalAlignment horizontalAlignment)
        {
            Content = text;
            HorizontalAlignment = horizontalAlignment;
        }

        public Cell(object content)
            : this(new MultilineText(content.ToString()), HorizontalAlignment.Default)
        {
        }

        public Cell(object content, HorizontalAlignment horizontalAlignment)
            : this(new MultilineText(content.ToString()), horizontalAlignment)
        {
        }
    }
}