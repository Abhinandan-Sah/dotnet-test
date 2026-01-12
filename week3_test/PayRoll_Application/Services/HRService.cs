using PayRoll_Application.Models;

namespace PayRoll_Application.Services
{
    public class HRService
    {
        public void SendHRNotification(Employee emp, PaySlip slip)
        {
            Console.WriteLine($"[HR] Employee {emp.Name} (ID: {emp.Id}) salary processed. Net Pay: {slip.Net:C}");
        }
    }
}
