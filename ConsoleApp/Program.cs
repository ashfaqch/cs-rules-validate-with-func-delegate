using System;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var employee = new Employee { FullName = "", DateOfBirth = Convert.ToDateTime("01/01/2000"), Salary = 25000 };
            var result = employee.IsValid(employee);

            Console.WriteLine($"Is employee valid: {result.Item1}{(result.Item1 ? "" : ", Error Message: " + result.Item2)}");
            Console.ReadLine();
        }
    }

    public class Employee
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Salary { get; set; }

        public Tuple<bool, string> IsValid(Employee employee)
        {
            Func<Employee, Tuple<bool, string>>[] rules =
            {
                e =>  string.IsNullOrWhiteSpace(e.FullName) ? Tuple.Create(false, "Invalid Full Name") : Tuple.Create(true, ""),
                e =>  e.DateOfBirth.Year < 1900 ? Tuple.Create(false, "Invalid Date Of Birth") : Tuple.Create(true, ""),
                e =>  e.Salary <= 0 ? Tuple.Create(false, "Invalid Salary") : Tuple.Create(true, "")
            };

            Tuple<bool, string> result = new Tuple<bool, string>(true, "");
            foreach (var rule in rules)
            {
                result = rule(employee);
                if (!result.Item1)
                {
                    break;
                }
            }

            return result;
        }
    }
}