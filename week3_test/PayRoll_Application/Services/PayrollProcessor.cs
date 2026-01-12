using PayRoll_Application.Models;
using PayRoll_Application.Delegates;

namespace PayRoll_Application.Services
{
    public class PayrollProcessor
    {
        private List<Employee> employees = new List<Employee>();
        private List<PaySlip> paySlips = new List<PaySlip>();

        // Delegate event
        public SalaryProcessedDelegate OnSalaryProcessed;

        public void AddEmployee(Employee e)
        {
            employees.Add(e);
        }

        public void ProcessPayroll()
        {
            Console.WriteLine("\n=== Processing Payroll ===\n");
            
            foreach (var employee in employees)
            {
                // Polymorphic calls
                decimal gross = employee.GetGross();
                decimal deductions = employee.GetDeductions();
                decimal netPay = employee.CalculatePay();

                // Create PaySlip
                PaySlip slip = new PaySlip();
                slip.EmployeeId = employee.Id;
                slip.EmployeeName = employee.Name;
                slip.EmployeeType = employee.Type;
                slip.Gross = gross;
                slip.Deductions = deductions;
                slip.Net = netPay;

                paySlips.Add(slip);

                // Fire delegate
                if (OnSalaryProcessed != null)
                {
                    OnSalaryProcessed(employee, slip);
                }
            }
        }

        public void PrintReport()
        {
            Console.WriteLine("\n=== PAYROLL REPORT ===\n");
            Console.WriteLine("ID, Name, Type, Gross, Deductions, Net");
            Console.WriteLine("-------------------------------------------------------------");

            foreach (var slip in paySlips)
            {
                Console.WriteLine(slip.EmployeeId + ", " + slip.EmployeeName + ", " + slip.EmployeeType + ", " + slip.Gross + ", " + slip.Deductions + ", " + slip.Net);
            }

            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("Total Employees: " + paySlips.Count);
            Console.WriteLine("Total Net Payout: " + paySlips.Sum(p => p.Net));
        }
    }
}
