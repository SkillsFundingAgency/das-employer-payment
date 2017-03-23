﻿using SFA.DAS.Events.Api.Types;

namespace SFA.DAS.EAS.Account.Api.Types.Events.Account
{
    public class AccountRenamedEvent : IEventView
    {
        public long Id { get; set; }
        public string Event { get; set; }
        public string ResourceUri { get; set; }
    }
}