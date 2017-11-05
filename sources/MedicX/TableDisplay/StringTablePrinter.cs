using System.Text;

namespace DustInTheWind.MedicX.TableDisplay
{
    public class StringTablePrinter : ITablePrinter
    {
        private readonly StringBuilder sb;

        public StringTablePrinter()
        {
            sb = new StringBuilder();
        }

        public void WriteBorder(string text)
        {
            sb.Append(text);
        }

        public void WriteLineBorder(string text)
        {
            sb.AppendLine(text);
        }

        public void WriteTitle(string text)
        {
            sb.Append(text);
        }

        public void WriteLineTitle(string text)
        {
            sb.AppendLine(text);
        }

        public void WriteHeader(string text)
        {
            sb.Append(text);
        }

        public void WriteLineHeader(string text)
        {
            sb.AppendLine(text);
        }

        public void WriteNormal(string text)
        {
            sb.Append(text);
        }

        public void WriteLineNormal(string text)
        {
            sb.AppendLine(text);
        }

        public void WriteLine()
        {
            sb.AppendLine();
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}