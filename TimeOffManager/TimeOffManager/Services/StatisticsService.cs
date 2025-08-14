using System;
using System.Collections.Generic;
using System.Linq;
using TimeOffManager.Models;

namespace TimeOffManager.Services
{
    public class StatisticsService
    {
        public void ShowTeamStats(List<Employee> employees)
        {
            if (employees == null || employees.Count == 0)
            {
                Console.WriteLine("Brak danych o pracownikach.");
                return;
            }

            int total = employees.Count;
            int onLeave = employees.Count(e => e.IsOnLeave(DateTime.Today));
            int available = total - onLeave;
            int percentAvailable = (int)((double)available / total * 100);

            Console.WriteLine("=== Statystyki zespołu ===");
            Console.WriteLine($"🟢 Dostępnych: {available}");
            Console.WriteLine($"🔴 Na urlopie: {onLeave}");
            Console.WriteLine($"📊 Obłożenie: {percentAvailable}%");
        }

        public void ShowIndividualStats(Employee employee)
        {
            if (employee == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[X] Nieprawidłowy pracownik.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"📋 Statystyki dla: {employee.Name} {employee.Surname}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"🗓️ Dostępne dni urlopowe: {employee.AvailableDays}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"📁 Liczba wniosków: {employee.LeaveHistory.Count}");
            Console.ResetColor();

            int approved = employee.LeaveHistory.Count(r => r.Status == LeaveStatus.Approved);
            int rejected = employee.LeaveHistory.Count(r => r.Status == LeaveStatus.Rejected);
            int pending = employee.LeaveHistory.Count(r => r.Status == LeaveStatus.Pending);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Zatwierdzone: {approved}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Odrzucone: {rejected}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"⏳ Oczekujące: {pending}");
            Console.ResetColor();
        }

    }
}
