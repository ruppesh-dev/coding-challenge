using System;

namespace PetPalsManagementSystem.exception
{
    public class AdoptionException : Exception
    {
        public AdoptionException(string message) : base(message) { }
    }
}
