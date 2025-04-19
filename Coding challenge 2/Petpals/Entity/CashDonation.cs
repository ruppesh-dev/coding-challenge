namespace PetPalsManagementSystem.entity
{
    public class CashDonation
    {
        public string DonorName { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonationDate { get; set; }

        public CashDonation(string donorName, decimal amount, DateTime donationDate)
        {
            DonorName = donorName;
            Amount = amount;
            DonationDate = donationDate;
        }
    }
}
