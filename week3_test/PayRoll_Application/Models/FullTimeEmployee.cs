namespace PayRoll_Application.Models
{
    public class FullTimeEmployee: Employee
    {
        public decimal Bonus {get; set;}
        public decimal PF {get; set;}
        public decimal Tax {get; set;}

        public FullTimeEmployee(int id, string name, decimal baseSalary, decimal bonus, decimal pf, decimal tax)
            : base(id, name, "FullTime")
        {
            BaseSalary = baseSalary;
            Bonus = bonus;
            PF = pf;
            Tax = tax;
        }

        public override decimal CalculatePay()
        {
            decimal gross = BaseSalary + Bonus;
            Deductions = PF + Tax;
            return gross - Deductions;
        }

        public override decimal GetGross()
        {
            return BaseSalary + Bonus;
        }

        public override decimal GetDeductions()
        {
            return PF + Tax;
        }
    }
}
