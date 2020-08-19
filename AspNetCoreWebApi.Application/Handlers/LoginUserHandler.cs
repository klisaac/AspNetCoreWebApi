using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using MediatR;
using AutoMapper;
using AspNetCoreWebApi.Core.Logging;
using AspNetCoreWebApi.Core.Specifications;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Application.Common.Helpers;
using AspNetCoreWebApi.Application.Commands;

namespace AspNetCoreWebApi.Application.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<LoginUserHandler> _logger;
        public LoginUserHandler(IUserRepository userRepository, IMapper mapper, IAppLogger<LoginUserHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetSingleAsync(new UserSpecification(request.UserName));

            // check if username exists and the password is correct
            if ((user == null) || (!Password.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt)))
            {
                _logger.LogInformation($"Username or password is incorrect");
                return false;
            }
            else
                return true;
        }
    }
}
