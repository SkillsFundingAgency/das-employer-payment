using System;

namespace SFA.DAS.EmployerPayments.Application
{
    public class NotFoundException : Exception
    {
        public string ErrorMessage { get; set; }

        public NotFoundException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }   
    }
}