using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Application.Queries;
using AspNetCoreWebApi.Application.Responses;
using System.Collections.Generic;
using AspNetCoreWebApi.Core.Specifications;

namespace AspNetCoreWebApi.Application.Handlers
{
    public class GetOrdersByCustomerIdHandler : IRequestHandler<GetOrdersByCustomerByIdQuery, OrderResponse>
    {
        private readonly IOrderRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public GetOrdersByCustomerIdHandler(IOrderRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OrderResponse> Handle(GetOrdersByCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<OrderResponse>(await _orderItemRepository.GetAsync(new OrderSpecification(request.CustomerId)));
        }
    }
}
