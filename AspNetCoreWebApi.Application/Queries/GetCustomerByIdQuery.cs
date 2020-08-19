using MediatR;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerResponse>
    {
        public int CustomerId { get; set; }

        public GetCustomerByIdQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
