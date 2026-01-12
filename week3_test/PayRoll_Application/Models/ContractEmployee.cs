namespace PayRoll_Application.Models
{
    public class ContractEmployee: Employee
    {
        public decimal DailyRate {get; set;}
        public int WorkingDays {get; set;}

        public ContractEmployee(int id, string name, decimal dailyRate, int workingDays, decimal deductions)
            : base(id, name, "Contract")
        {
            DailyRate = dailyRate;
            WorkingDays = workingDays;
            Deductions = deductions;
        }

        public override decimal CalculatePay()
        {
            decimal gross = DailyRate * WorkingDays;
            return gross - Deductions;
        }

        public override decimal GetGross()
        {
            return DailyRate * WorkingDays;
        }

        public override decimal GetDeductions()
        {
            return Deductions;
        }
    }
}
