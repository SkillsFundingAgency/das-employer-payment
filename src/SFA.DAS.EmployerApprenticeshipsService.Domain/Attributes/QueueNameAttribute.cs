using System;

namespace SFA.DAS.EmployerPayments.Domain.Attributes
{
    public class QueueNameAttribute : Attribute
    {
        public QueueNameAttribute(string connectionKey = "")
        {
            
        }
    }
}
