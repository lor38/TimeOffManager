using System;
using System.IO;
using TimeOffManager.Models;

namespace TimeOffManager.Services
{
    public static class FileManager
    {
        public static void ExportLeaveHistory(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            var fileName = $"LeaveHistory_{employee.Name}_{employee.Surname}.txt";
            using var writer = new StreamWriter(fileName);

            writer.WriteLine($"Historia urlopów: {employee.Name} {employee.Surname}");
            writer.WriteLine($"Dostępne dni urlopowe: {employee.AvailableDays}");
            writer.WriteLine("--------------------------------------------------");

            foreach (var request in employee.LeaveHistory)
            {
                writer.WriteLine(request.ToString());
            }

            Console.WriteLine($"✅ Historia urlopów zapisana do pliku: {fileName}");
        }

        public static void ExportTeamReport(List<Employee> employees)
        {
            var fileName = $"TeamReport_{DateTime.Today:yyyyMMdd}.txt";
            using var writer = new StreamWriter(fileName);

            writer.WriteLine("Raport zespołu - dostępność pracowników");
            writer.WriteLine($"Data: {DateTime.Today:yyyy-MM-dd}");
            writer.WriteLine("--------------------------------------------------");

            foreach (var emp in employees)
            {
                string status = emp.IsOnLeave(DateTime.Today) ? "Na urlopie" : "Dostępny";
                writer.WriteLine($"{emp.Name} {emp.Surname} - {status}");
            }

            Console.WriteLine($"📄 Raport zespołu zapisany do pliku: {fileName}");
        }
    }
}
