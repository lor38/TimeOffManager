using System;
using System.Collections.Generic;
using TimeOffManager.Models;
using TimeOffManager.Interfaces;
using TimeOffManager.Events;
using TimeOffManager.Services;

namespace TimeOffManager.Services
{
    public class LeaveManager
    {
        public List<Employee> Employees { get; } = new();

        public event LeaveRequestSubmittedDelegate? LeaveRequestSubmitted;

        public void LoadData()
        {
            Employees.Clear();
            Employees.AddRange(DataStorage.LoadEmployees());
            DataStorage.LoadRequests(Employees);
        }

        public void SaveData()
        {
            DataStorage.SaveEmployees(Employees);
            var allRequests = GetAllRequests();
            DataStorage.SaveRequests(allRequests);
        }

        public void AddEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            Employees.Add(employee);
            SaveData();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Dodano pracownika: {employee.Name} {employee.Surname}");
            Console.ResetColor();
        }

        public void SubmitRequest(Employee employee, LeaveRequest request)
        {
            if (employee == null || request == null)
                throw new ArgumentException("Nieprawidłowy pracownik lub wniosek.");

            if (request.Duration > employee.AvailableDays && request.Type == LeaveType.Wypoczynkowy)
                throw new InvalidOperationException("Brak wystarczającej liczby dni urlopowych.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📨 Przesyłanie wniosku urlopowego...");
            Console.ResetColor();

            switch (request.Type)
            {
                case LeaveType.Wypoczynkowy:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case LeaveType.Chorobowy:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LeaveType.Okolicznościowy:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
            }
            Console.WriteLine($"📝 Typ urlopu: {request.Type}");
            Console.ResetColor();

            employee.LeaveHistory.Add(request);
            LeaveRequestSubmitted?.Invoke(this, EventArgs.Empty);
            SaveData();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Wniosek dodany dla {employee.Name} {employee.Surname}");
            Console.WriteLine($"📅 Okres: {request.StartDate:dd-MM-yyyy} → {request.EndDate:dd-MM-yyyy}");
            Console.WriteLine($"🗓️ Pozostało dni urlopowych: {employee.AvailableDays - request.Duration}");
            Console.ResetColor();
        }

        public void ApproveRequest(ILeaveApprover approver, LeaveRequest request)
        {
            if (approver == null || request == null)
                throw new ArgumentException("Nieprawidłowy przełożony lub wniosek.");

            approver.ApproveRequest(request);
            SaveData();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✅ Wniosek zatwierdzony: {request.RequestedBy.Name} {request.RequestedBy.Surname}");
            Console.ResetColor();
        }

        public void RejectRequest(ILeaveApprover approver, LeaveRequest request)
        {
            if (approver == null || request == null)
                throw new ArgumentException("Nieprawidłowy przełożony lub wniosek.");

            approver.RejectRequest(request);
            SaveData();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Wniosek odrzucony: {request.RequestedBy.Name} {request.RequestedBy.Surname}");
            Console.ResetColor();
        }

        public List<LeaveRequest> GetAllRequests()
        {
            var allRequests = new List<LeaveRequest>();
            foreach (var emp in Employees)
            {
                allRequests.AddRange(emp.LeaveHistory);
            }
            return allRequests;
        }

        public List<Employee> GetEmployeesOnLeave(DateTime date)
        {
            return Employees.FindAll(e => e.IsOnLeave(date));
        }

        public List<Employee> GetAvailableEmployees(DateTime date)
        {
            return Employees.FindAll(e => !e.IsOnLeave(date));
        }
    }
}
