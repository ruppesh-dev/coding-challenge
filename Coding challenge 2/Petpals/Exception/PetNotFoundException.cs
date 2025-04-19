using System;

namespace PetPalsManagementSystem.exception
{
    public class PetNotFoundException : Exception
    {
        public PetNotFoundException(string message = "Pet not found in the system.") : base(message)
        {
            Console.WriteLine($"PetNotFoundException thrown: {message}");
        }
    }
}
