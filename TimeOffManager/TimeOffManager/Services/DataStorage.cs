using System;
using System.Collections.Generic;
using System.IO;
using TimeOffManager.Models;

namespace TimeOffManager.Services
{
    public static class DataStorage
    {
        private const string EmployeeFile = "Data/Employees.txt";
        private const string RequestFile = "Data/Requests.txt";

        public static void SaveEmployees(List<Employee> employees)
        {
            Directory.CreateDirectory("Data");
            using var writer = new StreamWriter(EmployeeFile, false);

            foreach (var emp in employees)
            {
                string role = emp is Supervisor ? "Supervisor" : "Employee";
                writer.WriteLine($"{emp.Name};{emp.Surname};{emp.AvailableDays};{role}");
            }
        }

        public static List<Employee> LoadEmployees()
        {
            var list = new List<Employee>();
            if (!File.Exists(EmployeeFile)) return list;

            foreach (var line in File.ReadAllLines(EmployeeFile))
            {
                var parts = line.Split(';');
                if (parts.Length >= 4)
                {
                    var name = parts[0];
                    var surname = parts[1];
                    var days = int.Parse(parts[2]);
                    var role = parts[3];

                    Employee emp = role == "Supervisor"
                        ? new Supervisor(name, surname)
                        : new Employee(name, surname);

                    emp.AvailableDays = days;
                    list.Add(emp);
                }
            }

            return list;
        }

        public static void SaveRequests(List<LeaveRequest> requests)
        {
            Directory.CreateDirectory("Data");
            using var writer = new StreamWriter(RequestFile, false);

            foreach (var r in requests)
            {
                writer.WriteLine($"{r.RequestedBy.Name};{r.RequestedBy.Surname};{r.StartDate:yyyy-MM-dd};{r.EndDate:yyyy-MM-dd};{r.Type};{r.Status};{r.Comment}");
            }
        }

        public static List<LeaveRequest> LoadRequests(List<Employee> employees)
        {
            var list = new List<LeaveRequest>();
            if (!File.Exists(RequestFile)) return list;

            foreach (var line in File.ReadAllLines(RequestFile))
            {
                var parts = line.Split(';');
                if (parts.Length >= 7)
                {
                    var name = parts[0];
                    var surname = parts[1];
                    var start = DateTime.Parse(parts[2]);
                    var end = DateTime.Parse(parts[3]);
                    var type = Enum.Parse<LeaveType>(parts[4]);
                    var status = Enum.Parse<LeaveStatus>(parts[5]);
                    var comment = parts[6];

                    var emp = employees.Find(e => e.Name == name && e.Surname == surname);
                    if (emp != null)
                    {
                        var request = new LeaveRequest
                        {
                            RequestedBy = emp,
                            StartDate = start,
                            EndDate = end,
                            Type = type,
                            Status = status,
                            Comment = comment
                        };
                        emp.LeaveHistory.Add(request);
                        list.Add(request);
                    }
                }
            }

            return list;
        }
    }
}
