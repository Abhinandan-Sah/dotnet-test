using PayRoll_Application.Models;
using PayRoll_Application.Services;

// Create services
HRService hrService = new HRService();
FinanceService financeService = new FinanceService();

// Create PayrollProcessor
PayrollProcessor processor = new PayrollProcessor();

// Subscribe to delegate
processor.OnSalaryProcessed += hrService.SendHRNotification;
processor.OnSalaryProcessed += financeService.SendFinanceNotification;

// Create FullTime Employees
FullTimeEmployee emp1 = new FullTimeEmployee(101, "John Doe", 50000, 5000, 2000, 8000);
FullTimeEmployee emp2 = new FullTimeEmployee(102, "Jane Smith", 60000, 7000, 2500, 10000);

// Create Contract Employees
ContractEmployee emp3 = new ContractEmployee(201, "Mike Johnson", 500, 22, 1000);
ContractEmployee emp4 = new ContractEmployee(202, "Sarah Williams", 600, 20, 800);

// Add employees to processor
processor.AddEmployee(emp1);
processor.AddEmployee(emp2);
processor.AddEmployee(emp3);
processor.AddEmployee(emp4);

// Process payroll (this will trigger delegate notifications)
processor.ProcessPayroll();

// Print final report
processor.PrintReport();

Console.WriteLine("\n✅ Payroll processing completed successfully!");
