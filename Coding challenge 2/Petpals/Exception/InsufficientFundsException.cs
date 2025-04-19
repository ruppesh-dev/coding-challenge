using System;

namespace PetPalsManagementSystem.exception
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message) : base(message) { }
    }
}
