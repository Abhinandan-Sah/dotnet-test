namespace PayRoll_Application.Models
{
    public class PaySlip
    {
        public int EmployeeId {get; set;}
        public string EmployeeName {get; set;}
        public string EmployeeType {get; set;}
        public decimal Gross {get; set;}
        public decimal Deductions {get; set;}
        public decimal Net {get; set;}
    }
}
