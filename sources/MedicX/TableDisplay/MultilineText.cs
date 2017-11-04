using System;
using System.Collections.Generic;
using System.Linq;

namespace DustInTheWind.MedicX.TableDisplay
{
    /// <summary>
    /// Represents a text on multiple lines.
    /// </summary>
    public class MultilineText
    {
        /// <summary>
        /// Gets the text as a single line.
        /// </summary>
        public string RawText { get; }

        /// <summary>
        /// Gets the text splitted in lines.
        /// </summary>
        public string[] Lines { get; }

        /// <summary>
        /// Gets the size of the smallest rectangle in which the text will fit.
        /// </summary>
        public Size Size { get; }

        public static MultilineText Empty { get; } = new MultilineText(string.Empty);

        /// <summary>
        /// Used only internaly when splitting the string in multiple lines.
        /// </summary>
        private enum LineEndChar
        {
            None,
            Cr,
            Lf
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultilineText"/> class with
        /// the raw text.
        /// </summary>
        /// <param name="text">The text as a single line.</param>
        /// <exception cref="ApplicationException"></exception>
        public MultilineText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                RawText = string.Empty;
                Lines = new string[0];
                Size = new Size(0, 0);
            }
            else
            {
                try
                {
                    int width = 0;
                    int startLineIndex = 0;
                    LineEndChar lastLineEndChar = LineEndChar.None;
                    List<string> lines = new List<string>();

                    int i;
                    for (i = 0; i < text.Length; i++)
                    {
                        if (text[i] == '\r')
                        {
                            // A line is end.
                            int lineWidth = i - startLineIndex;
                            lines.Add(text.Substring(startLineIndex, lineWidth));
                            if (lineWidth > width) width = lineWidth;

                            startLineIndex = i + 1;
                            lastLineEndChar = LineEndChar.Cr;
                        }
                        else if (text[i] == '\n')
                        {
                            if (i > startLineIndex || lastLineEndChar != LineEndChar.Cr)
                            {
                                // A line is end.
                                int lineWidth = i - startLineIndex;
                                lines.Add(text.Substring(startLineIndex, lineWidth));
                                if (lineWidth > width) width = lineWidth;
                            }

                            startLineIndex = i + 1;
                            lastLineEndChar = LineEndChar.Lf;
                        }
                    }

                    // Process the remaining text.

                    {
                        int lineWidth = i - startLineIndex;
                        lines.Add(text.Substring(startLineIndex, lineWidth));
                        if (lineWidth > width) width = lineWidth;
                    }

                    RawText = text;
                    Lines = lines.ToArray();
                    Size = new Size(width, lines.Count);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Error splitting the text in multiple lines.", ex);
                }
            }
        }

        public MultilineText(List<string> lines)
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));

            RawText = string.Join(string.Empty, lines);
            Lines = lines.ToArray();

            int width = lines.Max(x => x.Length);
            int height = lines.Count;
            Size = new Size(width, height);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            MultilineText multilineText = obj as MultilineText;

            if (multilineText != null)
                return RawText == multilineText.RawText;

            return false;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return RawText.GetHashCode();
        }
    }
}
