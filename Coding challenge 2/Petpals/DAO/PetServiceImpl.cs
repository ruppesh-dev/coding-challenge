using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using PetPalsManagementSystem.dao;
using PetPalsManagementSystem.entity;
using PetPalsManagementSystem.exception;
using PetPalsManagementSystem.util;

namespace PetPalsManagementSystem.dao
{
    public  class PetServiceImpl : IPetService
    {
        private string connStr = DBConnUtil.GetDBConnectionString();

        public  void AddPet(Pet pet)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "INSERT INTO Pets (Name, Age, Breed, AvailableForAdoption, Type, DogBreed, CatColor) " +
                                   "VALUES (@Name, @Age, @Breed, @AvailableForAdoption, @Type, @DogBreed, @CatColor)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", pet.Name);
                    cmd.Parameters.AddWithValue("@Age", pet.Age);
                    cmd.Parameters.AddWithValue("@Breed", pet.Breed);
                    cmd.Parameters.AddWithValue("@AvailableForAdoption", pet.AvailableForAdoption);
                    cmd.Parameters.AddWithValue("@Type", pet.Type);


                    if (pet is Dog dog)
                    {
                        cmd.Parameters.AddWithValue("@DogBreed", dog.DogBreed);
                        cmd.Parameters.AddWithValue("@CatColor", DBNull.Value);
                    }
                    else if (pet is Cat cat)
                    {
                        cmd.Parameters.AddWithValue("@CatColor", cat.CatColor);
                        cmd.Parameters.AddWithValue("@DogBreed", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DogBreed", DBNull.Value);
                        cmd.Parameters.AddWithValue("@CatColor", DBNull.Value);
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting pet: {ex.Message}");
            }
        }

        public void RemovePet(string petName) 
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT COUNT(1) FROM Pets WHERE Name = @Name";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", petName);

                conn.Open();
                int petCount = (int)cmd.ExecuteScalar(); 

                if (petCount == 0)
                {
                    throw new PetNotFoundException($"Pet with the name '{petName}' does not exist.");
                }

                
                string deleteQuery = "DELETE FROM Pets WHERE Name = @Name";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@Name", petName);

                deleteCmd.ExecuteNonQuery(); 
            }
        }



        public List<Pet> GetAvailablePets()
        {
            List<Pet> pets = new List<Pet>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT Name, Age, Breed, AvailableForAdoption, Type, DogBreed, CatColor FROM Pets WHERE AvailableForAdoption = 1";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Pet pet;
                    string petType = reader["Type"].ToString();

                    if (petType == "Dog")
                    {
                        pet = new Dog(
                            reader["Name"].ToString(),
                            Convert.ToInt32(reader["Age"]),
                            reader["Breed"].ToString(),
                            Convert.ToBoolean(reader["AvailableForAdoption"]),
                            reader["DogBreed"].ToString());
                    }
                    else if (petType == "Cat")
                    {
                        pet = new Cat(
                            reader["Name"].ToString(),
                            Convert.ToInt32(reader["Age"]),
                            reader["Breed"].ToString(),
                            Convert.ToBoolean(reader["AvailableForAdoption"]),
                            reader["CatColor"].ToString());
                    }
                    else
                    {
                        pet = new Pet(
                            reader["Name"].ToString(),
                            Convert.ToInt32(reader["Age"]),
                            reader["Breed"].ToString(),
                            Convert.ToBoolean(reader["AvailableForAdoption"]),
                            petType);
                    }

                    pets.Add(pet);
                }
            }
            return pets;
        }


    }

}
