using System;

namespace DustInTheWind.MedicX.GuiApplication
{
    internal class NoProjectException : Exception
    {
        private const string DefaultMessage = "There is no project loaded.";

        public NoProjectException()
            : base(DefaultMessage)
        {
        }
    }
}