using System;

namespace SFA.DAS.EmployerPayments.Domain.Attributes
{
    public class ServiceBusConnectionKeyAttribute : Attribute
    {
        public ServiceBusConnectionKeyAttribute(string connectionKey = "")
        {
            
        }
    }
}