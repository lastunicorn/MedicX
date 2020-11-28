using System;
using System.Linq;

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

        private static string[] ParseNames(string names)
        {
            return names.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();
        }

        public bool IsMatch(string name)
        {
            return names.Contains(name);
        }
    }
}