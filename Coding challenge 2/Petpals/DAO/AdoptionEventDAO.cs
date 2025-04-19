using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using PetPalsManagementSystem.Entity;
using PetPalsManagementSystem.util;

namespace PetPalsManagementSystem.dao
{
    public class AdoptionEventDAOImpl : IAdoptionEventDAO
    {
        private string connStr = DBConnUtil.GetDBConnectionString();

        
        public List<AdoptionEvent> GetUpcomingEvents()
        {
            List<AdoptionEvent> events = new List<AdoptionEvent>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT EventID, EventName, EventDate, Location FROM AdoptionEvents ORDER BY EventDate";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AdoptionEvent evt = new AdoptionEvent
                    {
                        EventID = Convert.ToInt32(reader["EventID"]),
                        EventName = reader["EventName"].ToString(),
                        EventDate = Convert.ToDateTime(reader["EventDate"]),
                        Location = reader["Location"].ToString()
                    };

                    events.Add(evt);
                }

                reader.Close();
            }

            return events;
        }

        public void RegisterParticipant(string participantName, string participantType, int eventId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO Participants (ParticipantName, ParticipantType, EventID) VALUES (@name, @type, @eventId)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", participantName);
                cmd.Parameters.AddWithValue("@type", participantType);
                cmd.Parameters.AddWithValue("@eventId", eventId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
