using System;
using Microsoft.Data.SqlClient;
using PetPalsManagementSystem.dao;
using PetPalsManagementSystem.entity;
using PetPalsManagementSystem.util;

namespace PetPalsManagementSystem.dao
{
    public class DonationServiceImpl : IDonationService
    {
        private string connStr = DBConnUtil.GetDBConnectionString();

        public void RecordCashDonation(CashDonation donation)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO Donations (DonorName, DonationAmount, DonationDate, DonationType) VALUES (@DonorName, @DonationAmount, @DonationDate, 'Cash')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DonorName", donation.DonorName);
                cmd.Parameters.AddWithValue("@DonationAmount", donation.Amount); 
                cmd.Parameters.AddWithValue("@DonationDate", donation.DonationDate);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
    }
}
