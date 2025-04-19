using System;

namespace PetPalsManagementSystem.exception
{
    public class InvalidPetAgeException : Exception
    {
        public InvalidPetAgeException(string message) : base(message) { }
    }
}
