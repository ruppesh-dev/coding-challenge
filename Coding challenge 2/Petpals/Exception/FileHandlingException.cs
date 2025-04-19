using System;

namespace PetPalsManagementSystem.exception
{
    public class FileHandlingException : Exception
    {
        public FileHandlingException(string message) : base(message) { }
    }
}
