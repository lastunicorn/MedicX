namespace DustInTheWind.MedicX.TableDisplay
{
    public interface ITablePrinter
    {
        void WriteBorder(string text);
        void WriteLineBorder(string text);

        void WriteTitle(string text);
        void WriteLineTitle(string text);

        void WriteHeader(string text);
        void WriteLineHeader(string text);

        void WriteNormal(string text);
        void WriteLineNormal(string text);
        void WriteLine();
    }
}