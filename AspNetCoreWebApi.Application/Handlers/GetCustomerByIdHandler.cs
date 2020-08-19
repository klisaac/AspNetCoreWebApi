using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Application.Queries;
using AspNetCoreWebApi.Application.Responses;
using AspNetCoreWebApi.Core.Specifications;

namespace AspNetCoreWebApi.Application.Handlers
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CustomerResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<CustomerResponse>(await _customerRepository.GetSingleAsync(new CustomerSpecification(request.CustomerId)));
        }
    }
}
