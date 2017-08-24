﻿using System;
using SFA.DAS.TimeProvider;

namespace SFA.DAS.EmployerPayments.Application.UnitTests
{
    public class FakeTimeProvider : DateTimeProvider
    {
        public FakeTimeProvider(DateTime input)
        {
            UtcNow = input;
        }

        public override DateTime UtcNow { get; }
    }
}