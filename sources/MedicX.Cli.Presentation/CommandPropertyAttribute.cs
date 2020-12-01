using System;

namespace MedicX.Cli.Presentation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    internal class CommandPropertyAttribute : Attribute
    {
        public string Name { get; set; }
    }
}