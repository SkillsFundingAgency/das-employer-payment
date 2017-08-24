using MediatR;

namespace SFA.DAS.EmployerPayments.Application.Queries.GetProvider
{
    public class GetProviderQueryRequest : IAsyncRequest<GetProviderQueryResponse>
    {
        public long ProviderId { get; set; }
    }
}