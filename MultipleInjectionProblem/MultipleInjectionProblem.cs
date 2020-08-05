using System;
using System.Collections.Generic;
using Common;
using Microsoft.Extensions.DependencyInjection;

namespace MsDiProblems
{
    public class MultipleInjectionProblem
    {
        public void MultipleInjectionProblemTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IEmployeeTaxCalculator, BetterEmployeeTaxCalculator>();

            serviceCollection.AddTransient<IEmployeeService, EmployeeService>();

            /// 200 loc
            serviceCollection.AddTransient<IEmployeeTaxCalculator, DefaultEmployeeTaxCalculator>();

            var provider = serviceCollection.BuildServiceProvider();

            var empService =(IEmployeeService) provider.GetService(typeof(IEmployeeService));
            empService.PaySalaries();
        }
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeTaxCalculator _employeeTaxCalculator;

        public EmployeeService(IEmployeeTaxCalculator employeeTaxCalculator)
        {
            _employeeTaxCalculator = employeeTaxCalculator;
        }
        
        public void PaySalaries()
        {
            var emps = ImagineThisIsACallToTheDb();

            foreach (var employee in emps)
            {
                PayEmployeeSalary(employee.Name, employee.Salary - _employeeTaxCalculator.CalculateTax(employee.Salary));
            }
        }

        private void PayEmployeeSalary(string name, decimal employeeSalary)
        {
            // imaginary thing that pays employees
            Console.WriteLine($"Paid {name} {employeeSalary}");
        }

        private List<Employee> ImagineThisIsACallToTheDb()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Name = "Joe",
                    Salary = 1000
                }
            };
        }
    }

    public class BetterEmployeeTaxCalculator : IEmployeeTaxCalculator
    {
        public decimal CalculateTax(decimal salary) => salary * 0.2m;
    }

    public class DefaultEmployeeTaxCalculator : IEmployeeTaxCalculator
    {
        public decimal CalculateTax(decimal salary) => 0;
    }

    public interface IEmployeeTaxCalculator
    {
        decimal CalculateTax(decimal salary);
    }
}