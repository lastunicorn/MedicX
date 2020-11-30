using System;

namespace DustInTheWind.MedicX.Application
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