using System.Collections.Generic;
using MediatR;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Queries
{
    public class GetOrdersByCustomerByIdQuery : IRequest<OrderResponse>
    {
        public int CustomerId { get; set; }

        public GetOrdersByCustomerByIdQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
