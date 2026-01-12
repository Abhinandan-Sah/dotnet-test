using PayRoll_Application.Models;

namespace PayRoll_Application.Services
{
    public class FinanceService
    {
        public void SendFinanceNotification(Employee emp, PaySlip slip)
        {
            Console.WriteLine($"[FINANCE] Payment of {slip.Net:C} approved for {emp.Name} (Type: {emp.Type})");
        }
    }
}
