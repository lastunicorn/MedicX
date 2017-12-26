using System;
using System.Collections.Generic;
using DustInTheWind.ConsoleTools;
using DustInTheWind.ConsoleTools.TabularData;
using DustInTheWind.MedicX.Common.Entities;
using DustInTheWind.MedicX.Persistence.Json;

namespace DustInTheWind.MedicX.Flows
{
    internal class DisplayClinicsFlow : IFlow
    {
        private readonly UnitOfWork unitOfWork;
        private readonly string clinicName;

        public DisplayClinicsFlow(UnitOfWork unitOfWork, string clinicName = null)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            this.unitOfWork = unitOfWork;
            this.clinicName = clinicName;
        }

        public void Run()
        {
            if (string.IsNullOrEmpty(clinicName))
                DisplayAllClinics();
            else
                SearchClinic();
        }

        private void DisplayAllClinics()
        {
            IClinicRepository clinicRepository = unitOfWork.ClinicRepository;

            List<Clinic> clinics = clinicRepository.GetAll();

            if (clinics == null || clinics.Count == 0)
                Console.WriteLine("No clinics exist in the database.");
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

        private void SearchClinic()
        {
            IClinicRepository clinicRepository = unitOfWork.ClinicRepository;

            List<Clinic> clinics = clinicRepository.Search(clinicName);

            if (clinics == null || clinics.Count == 0)
                Console.WriteLine("No clinics exist in the database contining the searched text.");
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