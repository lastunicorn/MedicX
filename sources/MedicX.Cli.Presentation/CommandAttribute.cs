using System;
using System.Linq;
using DustInTheWind.ConsoleTools;

namespace MedicX.Cli.Presentation
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class CommandAttribute : Attribute
    {
        private string[] names;

        public string Names
        {
            get => string.Join(",", names);
            set => names = value == null
                ? new string[0]
                : ParseNames(value);
        }

        public string Verb { get; set; }

        private static string[] ParseNames(string names)
        {
            return names.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();
        }

        public bool IsMatch(UserCommand userCommand)
        {
            bool matchName = names.Contains(userCommand.Name);

            if (!matchName)
                return false;

            string actualVerb = userCommand.Parameters.FirstOrDefault()?.Name;
            bool matchVerb = actualVerb == Verb;

            return matchVerb;
        }
    }
}