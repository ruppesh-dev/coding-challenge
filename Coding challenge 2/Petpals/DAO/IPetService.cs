using PetPalsManagementSystem.entity;
using System.Collections.Generic;

namespace PetPalsManagementSystem.dao
{
    public interface IPetService
    {
         void AddPet(Pet pet);
        void RemovePet(string petName);
        List<Pet> GetAvailablePets();  
    }
}
