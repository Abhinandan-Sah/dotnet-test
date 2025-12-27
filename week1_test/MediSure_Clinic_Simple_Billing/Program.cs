namespace MediSure_Clinic_Simple_Billing
{
    public class Program
    {
        public static void Main()
        {
            // Entry point for the MediSure Clinic Simple Billing application Problem
            int inp;
            
            // Main menu loop - continues until user selects Exit option
            do
            {
                // Display menu options
                Console.WriteLine("================== MediSure Clinic Billing ==================");
                Console.WriteLine("1. Create New Bill (Enter Patient Details)");
                Console.WriteLine("2. View Last Bill");
                Console.WriteLine("3. Clear Last Bill");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");
                
                // Validate menu option input - ensure it's an integer
                while (!int.TryParse(Console.ReadLine(), out inp))
                {
                    Console.WriteLine("Invalid Input. Enter valid input.");
                    Console.Write("Enter your option: ");
                }

                // Validate menu option range
                if (inp < 1 || inp > 4)
                {
                    Console.WriteLine("Invalid Input. Enter number from 1 to 4");
                }
                else
                {
                    // Option 1: Create new bill with patient details
                    if (inp == 1)
                    {
                        // Capture patient information
                        Console.Write("\nEnter Bill Id: ");
                        string? BillId = Console.ReadLine();

                        Console.Write("Enter Patient Name: ");
                        string? PatientName = Console.ReadLine();

                        // Check insurance status
                        Console.Write("Is the patient insured? (Y/N): ");
                        string? insuranceInput = Console.ReadLine()?.ToUpper();
                        bool HasInsurance = (insuranceInput == "Y");

                        // Capture and validate Consultation Fee
                        Console.Write("Enter Consultation Fee: ");
                        decimal ConsultationFee;
                        while (!decimal.TryParse(Console.ReadLine(), out ConsultationFee))
                        {
                            Console.WriteLine("Invalid Consultation Fee. Enter valid amount.");
                            Console.Write("Enter Consultation Fee: ");
                        }

                        // Capture and validate Lab Charges
                        Console.Write("Enter Lab Charges: ");
                        decimal LabCharges;
                        while (!decimal.TryParse(Console.ReadLine(), out LabCharges))
                        {
                            Console.WriteLine("Invalid Lab Charges. Enter valid amount.");
                            Console.Write("Enter Lab Charges: ");
                        }

                        // Capture and validate Medicine Charges
                        Console.Write("Enter Medicine Charges: ");
                        decimal MedicineCharges;
                        while (!decimal.TryParse(Console.ReadLine(), out MedicineCharges))
                        {
                            Console.WriteLine("Invalid Medicine Charges. Enter valid amount.");
                            Console.Write("Enter Medicine Charges: ");
                        }

                        // Create bill object and compute amounts
                        PatientBill patientBill = new PatientBill(BillId!, PatientName!, HasInsurance, ConsultationFee, LabCharges, MedicineCharges);
                        patientBill.CreateBill();
                    }
                    // Option 2: View the last created bill
                    else if (inp == 2)
                    {
                        PatientBill.ViewLastBill();
                    }
                    // Option 3: Clear the last bill from memory
                    else if (inp == 3)
                    {
                        PatientBill.ClearLastBill();
                    }
                }

                Console.WriteLine("------------------------------------------------------------");

            } while (inp != 4);

            Console.WriteLine("\nThank you. Application closed normally.");
        }
    }
}