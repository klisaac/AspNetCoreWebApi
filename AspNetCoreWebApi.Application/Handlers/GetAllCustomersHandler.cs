using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using AutoMapper;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Application.Queries;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Handlers
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CustomerResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<CustomerResponse>>(await _customerRepository.GetAllByIncludingAsync(c => c.DefaultAddress));
        }
    }
}
