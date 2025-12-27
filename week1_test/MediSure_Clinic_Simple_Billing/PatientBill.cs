namespace MediSure_Clinic_Simple_Billing
{
    /// <summary>
    /// Represents a patient bill with consultation, lab, and medicine charges
    /// Calculates gross amount, discount (10% for insured), and final payable
    /// </summary>
    public class PatientBill
    {
        // Patient and bill identification
        public string? BillId { get; set; }
        public string? PatientName { get; set; }
        public bool HasInsurance { get; set; }
        
        // Individual charge components
        public decimal ConsultationFee { get; set; }
        public decimal LabCharges { get; set; }
        public decimal MedicineCharges { get; set; }

        // Calculated amounts
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalPayable { get; set; }

        // Static storage for last bill (no arrays/lists allowed)
        public static PatientBill? LastBill { get; set; }
        public static bool HasLastBill { get; set; } = false;

        /// <summary>
        /// Constructor - validates inputs and initializes bill object
        /// </summary>
        public PatientBill(string BillId, string PatientName, bool HasInsurance, decimal ConsultationFee, decimal LabCharges, decimal MedicineCharges)
        {
            try
            {
                // Validate BillId is not empty
                if (string.IsNullOrWhiteSpace(BillId))
                {
                    throw new ArgumentException("Invalid input. BillId cannot be null or whitespace.");
                }
                // Consultation fee must be positive
                else if (ConsultationFee <= 0)
                {
                    throw new ArgumentException("ConsultationFee must be greater than 0.");
                }
                // Lab charges must be non-negative
                else if (LabCharges < 0)
                {
                    throw new ArgumentException("LabCharges must be greater than or equal to 0.");
                }
                // Medicine charges must be non-negative
                else if (MedicineCharges < 0)
                {
                    throw new ArgumentException("MedicineCharges must be greater than or equal to 0.");
                }

                this.BillId = BillId;
                this.PatientName = PatientName;
                this.HasInsurance = HasInsurance;
                this.ConsultationFee = ConsultationFee;
                this.LabCharges = LabCharges;
                this.MedicineCharges = MedicineCharges;
            }
            catch (Exception err)
            {
                Console.WriteLine("Error occurred: " + err.Message);
            }
        }

        /// <summary>
        /// Computes gross amount, discount (10% for insured), and final payable
        /// Displays results and stores as LastBill
        /// </summary>
        public void CreateBill()
        {
            // Calculate Gross Amount (sum of all charges)
            GrossAmount = ConsultationFee + LabCharges + MedicineCharges;

            // Calculate Discount Amount (10% if insured, otherwise 0)
            if (HasInsurance)
            {
                DiscountAmount = GrossAmount * 0.10m;
            }
            else
            {
                DiscountAmount = 0;
            }

            // Calculate Final Payable (gross minus discount)
            FinalPayable = GrossAmount - DiscountAmount;

            // Display results
            Console.WriteLine("\nBill created successfully.");
            Console.WriteLine($"Gross Amount: {Math.Round(GrossAmount, 2):F2}");
            Console.WriteLine($"Discount Amount: {Math.Round(DiscountAmount, 2):F2}");
            Console.WriteLine($"Final Payable: {Math.Round(FinalPayable, 2):F2}");

            // Store as last bill
            LastBill = this;
            HasLastBill = true;
        }

        /// <summary>
        /// Displays the last created bill details
        /// Shows message if no bill exists
        /// </summary>
        public static void ViewLastBill()
        {
            // Check if a bill exists in memory
            if (!HasLastBill || LastBill == null)
            {
                Console.WriteLine("No bill available. Please create a new bill first.");
                return;
            }

            Console.WriteLine("\n----------- Last Bill -----------");
            Console.WriteLine($"BillId: {LastBill.BillId}");
            Console.WriteLine($"Patient: {LastBill.PatientName}");
            Console.WriteLine($"Insured: {(LastBill.HasInsurance ? "Yes" : "No")}");
            Console.WriteLine($"Consultation Fee: {LastBill.ConsultationFee:F2}");
            Console.WriteLine($"Lab Charges: {LastBill.LabCharges:F2}");
            Console.WriteLine($"Medicine Charges: {LastBill.MedicineCharges:F2}");
            Console.WriteLine($"Gross Amount: {LastBill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {LastBill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {LastBill.FinalPayable:F2}");
            Console.WriteLine("--------------------------------");
        }

        /// <summary>
        /// Clears the last bill from memory
        /// </summary>
        public static void ClearLastBill()
        {
            LastBill = null;
            HasLastBill = false;
            Console.WriteLine("Last bill cleared.");
        }
    }
}
