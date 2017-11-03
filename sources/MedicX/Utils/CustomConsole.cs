using System;

namespace DustInTheWind.MedicX.Utils
{
    internal class CustomConsole
    {
        #region Emphasies

        public static void WriteEmphasies(string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }
        public static void WriteLineEmphasies(string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }

        #endregion

        #region Success

        public static void WriteSuccess(string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLineSuccess(string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLineSuccess(object o)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(o);
            Console.ForegroundColor = oldColor;
        }

        #endregion

        #region Warning

        public static void WriteWarning(string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLineWarning(string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLineWarning(string text, params object[] arg)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text, arg);
            Console.ForegroundColor = oldColor;
        }

        #endregion

        #region Error

        public static void WriteError(string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLineError(string text)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteLineError(object o)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(o);
            Console.ForegroundColor = oldColor;
        }

        public static void WriteError(Exception ex)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ForegroundColor = oldColor;
        }

        #endregion

        #region General

        public static void WriteLine()
        {
            Console.WriteLine();
        }

        public static void Write(string text)
        {
            Console.Write(text);
        }

        public static void Write(string text, params object[] arg)
        {
            Console.Write(text, arg);
        }

        public static void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public static void WriteLine(string text, params object[] arg)
        {
            Console.WriteLine(text, arg);
        }

        #endregion

        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}