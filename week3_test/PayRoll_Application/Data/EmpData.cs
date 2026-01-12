using PayRoll_Application.Models;

namespace PayRoll_Application.Data
{
    public class EmpData
    {
        static List<Employee> _employees = new List<Employee>();

        public EmpData()
        {
            // Add FullTime Employees
            _employees.Add(new FullTimeEmployee(101, "John Doe", 50000, 5000, 2000, 8000));
            _employees.Add(new FullTimeEmployee(102, "Jane Smith", 60000, 7000, 2500, 10000));

            // Add Contract Employees
            _employees.Add(new ContractEmployee(201, "Mike Johnson", 500, 22, 1000));
            _employees.Add(new ContractEmployee(202, "Sarah Williams", 600, 20, 800));
        }

        public List<Employee> GetEmployees()
        {
            return _employees;
        }

        public void Add(Employee emp)
        {
            _employees.Add(emp);
        }
        
    }
}