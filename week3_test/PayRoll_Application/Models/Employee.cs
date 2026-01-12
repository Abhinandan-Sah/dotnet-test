namespace PayRoll_Application.Models
{
    public abstract class Employee
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Type {get; set;}
        public decimal BaseSalary {get; set;}
        public decimal Deductions {get; set;}

        public Employee(int id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public abstract decimal CalculatePay();
        public abstract decimal GetGross();
        public abstract decimal GetDeductions();
    }
}
