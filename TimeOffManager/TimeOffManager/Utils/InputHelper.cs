using System;
using TimeOffManager.Models;

namespace TimeOffManager.Utils
{
    public static class InputHelper
    {
        public static string GetString(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        public static int GetInt(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;

                Console.WriteLine("❌ Wprowadź poprawną liczbę całkowitą.");
            }
        }

        public static DateTime GetDate(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (rrrr-mm-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    return date;

                Console.WriteLine("❌ Wprowadź poprawną datę.");
            }
        }

        public static LeaveType GetLeaveType()
        {
            Console.WriteLine("Wybierz typ urlopu:");
            Console.WriteLine("1. Wypoczynkowy");
            Console.WriteLine("2. Na żądanie");
            Console.WriteLine("3. Bezpłatny");

            int choice = GetInt("Twój wybór");
            return choice switch
            {
                1 => LeaveType.Wypoczynkowy,
                2 => LeaveType.NaZadanie,
                3 => LeaveType.Bezplatny,
                _ => LeaveType.Wypoczynkowy
            };
        }
    }
}
