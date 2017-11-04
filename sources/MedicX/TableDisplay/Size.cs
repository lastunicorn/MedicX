using System.Globalization;

namespace DustInTheWind.MedicX.TableDisplay
{
    /// <summary>
    /// Represents the size of a rectangle.
    /// Imutable.
    /// </summary>
    public struct Size
    {
        /// <summary>
        /// Gets the width component.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height component.
        /// </summary>
        public int Height { get; }

        public static Size Empty { get; } = new Size(0, 0);

        /// <summary>
        /// Initializes a new instance of the <see cref="Size"/> structure with
        /// the width and height values.
        /// </summary>
        /// <param name="width">The width component of the size.</param>
        /// <param name="height">The height component of the size.</param>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Size))
                return false;

            Size size = (Size)obj;
            return size.Width == Width && size.Height == Height;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return Width ^ Height;
        }

        /// <summary>
        /// Return the string representation of the current instance.
        /// </summary>
        /// <returns>The string representation of the current instance.</returns>
        public override string ToString()
        {
            string widthAsString = Width.ToString(CultureInfo.CurrentCulture);
            string heightAsString = Height.ToString(CultureInfo.CurrentCulture);

            return $"{{Width={widthAsString}, Height={heightAsString}}}";
        }
    }
}
