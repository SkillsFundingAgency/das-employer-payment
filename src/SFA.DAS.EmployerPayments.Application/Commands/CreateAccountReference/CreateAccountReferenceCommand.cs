using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SFA.DAS.EmployerPayments.Application.Commands.CreateAccountReference
{
    public class CreateAccountReferenceCommand : IAsyncRequest
    {
        public long AccountId { get; set; }
    }
}
