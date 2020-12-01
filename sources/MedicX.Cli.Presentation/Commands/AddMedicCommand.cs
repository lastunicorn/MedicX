using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.ConsoleTools;
using DustInTheWind.MedicX.CommandApplication.AddMedic;
using DustInTheWind.MedicX.RequestBusModel;

namespace MedicX.Cli.Presentation.Commands
{
    [Command(Names = "medic", Verb = "add")]
    internal class AddMedicCommand : ICommand
    {
        private readonly RequestBus requestBus;

        public AddMedicCommand(RequestBus requestBus)
        {
            this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));
        }

        public void Execute(UserCommand command)
        {
            AddMedicRequest request = new AddMedicRequest
            {
                Name = ExtractName(command),
                Specializations = ExtractSpecializations(command),
                Comments = ExtractComments(command)
            };
            requestBus.ProcessRequest(request).Wait();
        }

        private static string ExtractName(UserCommand command)
        {
            return command.Parameters.FirstOrDefault(x => x.Name == "name")?.Value;
        }

        private static List<string> ExtractSpecializations(UserCommand command)
        {
            string rawValue = command.Parameters.FirstOrDefault(x => x.Name == "specializations")?.Value;

            if (rawValue == null)
                return new List<string>();

            return rawValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();
        }

        private static string ExtractComments(UserCommand command)
        {
            return command.Parameters.FirstOrDefault(x => x.Name == "comments")?.Value;
        }
    }
}