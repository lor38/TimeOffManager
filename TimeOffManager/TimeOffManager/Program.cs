using System;
using System.Threading;
using TimeOffManager.Menu;

namespace TimeOffManager
{
    class Program
    {
        static void Main()
        {
            ShowIntro();

            var menu = new ConsoleMenu();
            menu.Run();
        }

        static void ShowIntro()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            string[] logo = new[]
            {
                " _______ _                                __  __                                    ",
                "|__   __| |                               |  \\/  |                                   ",
                "   | |  | |__   ___  _ __ ___   ___       | \\  / | __ _ _ __   __ _  ___ _ __ ___   ",
                "   | |  | '_ \\ / _ \\| '_ ` _ \\ / _ \\ _____| |\\/| |/ _` | '_ \\ / _` |/ _ \\ '__/ __|  ",
                "   | |  | | | | (_) | | | | | |  __/|_____| |  | | (_| | | | | (_| |  __/ |  \\__ \\  ",
                "   |_|  |_| |_|\\___/|_| |_| |_|\\___|      |_|  |_|\\__,_|_| |_|\\__, |\\___|_|  |___/  ",
                "                                                            __/ |                  ",
                "                                                           |___/                   "
            };

            foreach (var line in logo)
            {
                Console.WriteLine(line);
                Thread.Sleep(50); // efekt animacji
            }

            Console.ResetColor();
            Console.WriteLine("\n🗓️ TimeOffManager – Leave Management Console App\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("TimeOffManager to aplikacja konsolowa w C#, która umożliwia składanie i zarządzanie wnioskami urlopowymi.");
            Console.WriteLine("Projekt powstał w ramach wyzwania '21 dni z C#' na platformie edu.gotoit.pl.\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✨ Funkcje:");
            Console.WriteLine("• Składanie wniosków urlopowych z komentarzem i datami");
            Console.WriteLine("• Zatwierdzanie/odrzucanie przez przełożonego");
            Console.WriteLine("• Historia i statystyki urlopów");
            Console.WriteLine("• Eksport danych do pliku\n");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("🛠️ Technologie:");
            Console.WriteLine("• C# (.NET 9)");
            Console.WriteLine("• Programowanie obiektowe");
            Console.WriteLine("• Obsługa zdarzeń");
            Console.WriteLine("• Konsolowy interfejs użytkownika\n");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("👤 Autor: Robert Lorenc");
            Console.WriteLine("GitHub: https://github.com/lor38\n");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("📄 Licencja: MIT");
            Console.WriteLine("🚧 Status: Projekt w fazie rozwoju\n");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("📌 Planowane funkcje:");
            Console.WriteLine("• Powiadomienia e-mail o zatwierdzeniu wniosku");
            Console.WriteLine("• Interfejs graficzny (GUI)");
            Console.WriteLine("• Logowanie użytkowników");
            Console.WriteLine("• Integracja z kalendarzem Google\n");

            Console.ResetColor();
            Console.WriteLine("============================================================\n");
            Console.Write("Naciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
