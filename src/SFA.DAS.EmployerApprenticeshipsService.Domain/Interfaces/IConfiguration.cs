﻿using System.Collections.Generic;

namespace SFA.DAS.EmployerPayments.Domain.Interfaces
{
    public interface IConfiguration
    {
        string DatabaseConnectionString { get; set; }

        string ServiceBusConnectionString { get; set; }
        Dictionary<string,string> ServiceBusConnectionStrings { get; }
    }
}