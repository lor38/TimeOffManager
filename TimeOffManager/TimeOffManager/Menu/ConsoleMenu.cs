using System;
using System.Threading;
using TimeOffManager.Models;
using TimeOffManager.Services;
using TimeOffManager.Utils;

namespace TimeOffManager.Menu
{
    internal class ConsoleMenu
    {
        private readonly LeaveManager leaveManager = new();
        private readonly StatisticsService statsService = new();
        private Employee? emp;

        public void Run()
        {
            Console.Title = "System Urlopowy - TimeOffManager";
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            leaveManager.LoadData();

            emp = leaveManager.Employees.Find(e => e.Name == "Robert" && e.Surname == "Lorenc");

            if (emp is not Supervisor)
            {
                if (emp != null)
                    leaveManager.Employees.Remove(emp);

                emp = new Supervisor("Robert", "Lorenc");
                leaveManager.AddEmployee(emp);
            }


            leaveManager.LeaveRequestSubmitted += OnLeaveRequestSubmitted;

            ShowHeader();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.ResetColor();

                ShowMenu();

                Console.ForegroundColor = ConsoleColor.Yellow;
                int choice = InputHelper.GetInt("👉 Wybierz opcję: ");
                Console.ResetColor();

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("⏳ Przetwarzanie");
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(300);
                    Console.Write(".");
                }
                Console.WriteLine();
                Console.ResetColor();
                Thread.Sleep(300);

                Console.Clear();

                switch (choice)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("👥 Wyświetlanie pracowników...");
                        Console.ResetColor();
                        ShowEmployee();
                        break;

                    case 2:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("📝 Składanie wniosku urlopowego...");
                        Console.ResetColor();
                        SubmitRequest();
                        break;

                    case 3:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("📤 Przetwarzanie wniosku...");
                        Console.ResetColor();
                        ProcessRequest();
                        break;

                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("📊 Statystyki urlopowe...");
                        Console.ResetColor();
                        ShowStats();
                        break;

                    case 5:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("📁 Eksport historii...");
                        Console.ResetColor();
                        ExportHistory();
                        break;

                    case 0:
                        ShowGoodbye();
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[X] Nieprawidłowy wybór. Spróbuj ponownie.");
                        Console.ResetColor();
                        break;
                }

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("🔁 Naciśnij dowolny klawisz, aby wrócić do menu...");
                Console.ResetColor();
                Console.ReadKey();
            }
        }

        private void ShowHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("════════════════════════════════════════════════════════════");
            Console.WriteLine("  WITAMY W APLIKACJI System Urlopowy");
            Console.WriteLine("  Autor: Robert Lorenc");
            Console.WriteLine("  Projekt certyfikacyjny w ramach wyzwania \"21 Dni z C#\"");
            Console.WriteLine("  Platforma edukacyjna: edu.gotoit.pl");
            Console.WriteLine("════════════════════════════════════════════════════════════");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n🔁 Naciśnij dowolny klawisz, aby kontynuować...");
            Console.ResetColor();
            Console.ReadKey();
        }


        private void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine("║         MENU GŁÓWNE APLIKACJI        ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine("1. Pokaż dane pracownika");
            Console.WriteLine("2. Złóż wniosek urlopowy");
            Console.WriteLine("3. Zatwierdź/Odrzuć wniosek");
            Console.WriteLine("4. Pokaż statystyki");
            Console.WriteLine("5. Eksportuj historię urlopów");
            Console.WriteLine("0. Wyjście");
            Console.WriteLine("----------------------------------------");
        }

        private void ShowGoodbye()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("🔒 Zamykanie aplikacji");
            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(300);
                Console.Write(".");
            }
            Console.WriteLine("\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("════════════════════════════════════════════════════════════");
            Console.WriteLine("  Dziękujemy za skorzystanie z Systemu Urlopowego!");
            Console.WriteLine("  Do zobaczenia ! ");
            Console.WriteLine("════════════════════════════════════════════════════════════");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n👋 Naciśnij dowolny klawisz, aby zamknąć...");
            Console.ResetColor();
            Console.ReadKey();
        }


        private void ShowEmployee()
        {
            if (emp == null)
            {
                Console.WriteLine("[X] Brak pracownika.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("══════════════════════════════════════════════");
            Console.WriteLine("              DANE PRACOWNIKA                ");
            Console.WriteLine("══════════════════════════════════════════════");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Imię i nazwisko: {emp.Name} {emp.Surname}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Dostępne dni urlopowe: {emp.AvailableDays}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Liczba wniosków: {emp.LeaveHistory.Count}");
            Console.ResetColor();

            Console.WriteLine("══════════════════════════════════════════════");
        }


        private void SubmitRequest()
        {
            if (emp == null)
            {
                Console.WriteLine("Brak pracownika.");
                return;
            }

            Console.WriteLine("=== Składanie wniosku urlopowego ===");

            var request = new LeaveRequest
            {
                RequestedBy = emp,
                StartDate = InputHelper.GetDate("Data rozpoczęcia"),
                EndDate = InputHelper.GetDate("Data zakończenia"),
                Type = InputHelper.GetLeaveType(),
                Comment = InputHelper.GetString("Komentarz (opcjonalnie)")
            };

            try
            {
                leaveManager.SubmitRequest(emp, request);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[OK] Wniosek złożony.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[X] Błąd: {ex.Message}");
                Console.ResetColor();
            }
        }

        private void ProcessRequest()
        {
            Console.WriteLine("=== Rozpatrywanie wniosków ===");

            if (emp is not Supervisor supervisor)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[X] Brak uprawnień do zatwierdzania wniosków.");
                Console.ResetColor();
                return;
            }

            var pending = leaveManager.GetAllRequests().FindAll(r => r.Status == LeaveStatus.Pending);
            if (pending.Count == 0)
            {
                Console.WriteLine("Brak wniosków do rozpatrzenia.");
                return;
            }

            for (int i = 0; i < pending.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pending[i]}");
            }

            int index = InputHelper.GetInt("Wybierz numer wniosku") - 1;
            if (index < 0 || index >= pending.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nieprawidłowy numer.");
                Console.ResetColor();
                return;
            }

            var decision = InputHelper.GetString("Zatwierdzić (t) czy odrzucić (n)?").ToLower();
            if (decision == "t")
                leaveManager.ApproveRequest(supervisor, pending[index]);
            else
                leaveManager.RejectRequest(supervisor, pending[index]);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[OK] Wniosek rozpatrzony.");
            Console.ResetColor();
        }


        private void ShowStats()
        {
            if (emp == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[X] Brak pracownika.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("══════════════════════════════════════════════");
            Console.WriteLine("           STATYSTYKI PRACOWNIKA              ");
            Console.WriteLine("══════════════════════════════════════════════");
            Console.ResetColor();

            statsService.ShowIndividualStats(emp);

            Console.WriteLine("══════════════════════════════════════════════");
        }


        private void ExportHistory()
        {
            if (emp == null)
            {
                Console.WriteLine("[X] Brak pracownika.");
                return;
            }

            Console.WriteLine("=== Eksport historii urlopów ===");
            FileManager.ExportLeaveHistory(emp);
        }

        private void OnLeaveRequestSubmitted(object sender, EventArgs args)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[NEW] Złożono nowy wniosek urlopowy.");
            Console.ResetColor();
        }
    }
}
