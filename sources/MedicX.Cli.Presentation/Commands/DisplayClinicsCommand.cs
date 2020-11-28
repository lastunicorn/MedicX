using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.TabularData;
using DustInTheWind.MedicX.Domain.DataAccess;
using DustInTheWind.MedicX.Domain.Entities;

namespace MedicX.Cli.Presentation.Commands
{
    internal class DisplayClinicsCommand : ICommand
    {
        private readonly IUnitOfWork unitOfWork;

        public DisplayClinicsCommand(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public bool IsMatch(UserCommand command)
        {
            return command.Name == "clinic" || command.Name == "clinics";
        }

        public void Execute(UserCommand command)
        {
            if (command.Parameters.Count > 0)
            {
                string searchText = command.Parameters.ElementAt(0).Name;
                SearchClinic(searchText);
            }
            else
            {
                DisplayAllClinics();
            }
        }

        private void DisplayAllClinics()
        {
            IClinicRepository clinicRepository = unitOfWork.ClinicRepository;

            List<Clinic> clinics = clinicRepository.GetAll();

            if (clinics == null || clinics.Count == 0)
                CustomConsole.WriteLineError("No clinics exist in the database.");
            else
                DisplayClinicList(clinics);
        }

        private static void DisplayClinicList(IEnumerable<Clinic> clinics)
        {
            Table clinicsTable = CreateClinicsTable(clinics);

            CustomConsole.WriteLine("Clinics found: " + clinicsTable.RowCount);

            ConsoleTablePrinter tablePrinter = new ConsoleTablePrinter();
            clinicsTable.Render(tablePrinter);
        }

        private static Table CreateClinicsTable(IEnumerable<Clinic> clinics)
        {
            Table clinicsTable = new Table
            {
                Border = TableBorder.SingleLineBorder,
                DrawLinesBetweenRows = true,
                DisplayColumnHeaders = true
            };

            clinicsTable.Columns.Add(new Column("Name")
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Header = new MultilineText("Name")
            });

            clinicsTable.Columns.Add(new Column("Address")
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Header = new MultilineText("Address")
            });

            foreach (Clinic clinic in clinics)
            {
                string address = clinic.Address?.ToString() ?? string.Empty;

                clinicsTable.AddRow(new[]
                {
                    new Cell(clinic.Name),
                    new Cell(address)
                });
            }

            return clinicsTable;
        }

        private void SearchClinic(string searchText)
        {
            IClinicRepository clinicRepository = unitOfWork.ClinicRepository;

            List<Clinic> clinics = clinicRepository.Search(searchText);

            if (clinics == null || clinics.Count == 0)
                CustomConsole.WriteLineError("No clinics exist in the database containing the searched text.");
            else if (clinics.Count == 1)
                DisplayClinicDetails(clinics[0]);
            else
                DisplayClinicList(clinics);
        }

        private static void DisplayClinicDetails(Clinic clinic)
        {
            Table clinicTable = CreateClinicDetailsTable(clinic);

            ConsoleTablePrinter tablePrinter = new ConsoleTablePrinter();
            clinicTable.Render(tablePrinter);
        }

        private static Table CreateClinicDetailsTable(Clinic clinic)
        {
            Table medicsTable = new Table
            {
                Border = TableBorder.SingleLineBorder,
                DrawLinesBetweenRows = true,
                DisplayColumnHeaders = false
            };

            medicsTable.Columns.Add(new Column("Field")
            {
                HorizontalAlignment = HorizontalAlignment.Right
            });

            medicsTable.Columns.Add(new Column("Value")
            {
                HorizontalAlignment = HorizontalAlignment.Left
            });

            medicsTable.AddRow(new[]
            {
                new Cell("Name"),
                new Cell(clinic.Name)
            });

            if (clinic.Address != null)
                medicsTable.AddRow(new[]
                {
                    new Cell("Address"),
                    new Cell(clinic.Address.ToString())
                });

            if (clinic.Phones != null)
                medicsTable.AddRow(new[]
                {
                    new Cell("Phones"),
                    new Cell(clinic.Phones)
                });

            if (clinic.Program != null)
                medicsTable.AddRow(new[]
                {
                    new Cell("Program"),
                    new Cell(clinic.Program)
                });

            if (clinic.Comments != null)
                medicsTable.AddRow(new[]
                {
                    new Cell("Comments"),
                    new Cell(clinic.Comments)
                });

            return medicsTable;
        }
    }
}