using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;
using AutoMapper;
using MediatR;
using AspNetCoreWebApi.Application.Commands;
using AspNetCoreWebApi.Application.Common.Exceptions;
using AspNetCoreWebApi.Core.Entities;
using AspNetCoreWebApi.Core.Logging;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Core.Specifications;
using AspNetCoreWebApi.Application.Common.Helpers;
using AspNetCoreWebApi.Application.Common.Identity;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserRepository userRepository, ICurrentUser currentUser, IMapper mapper, IAppLogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetSingleAsync(new UserSpecification(request.UserName)) != null)
                throw new BadRequestException($"User, {request.UserName} already exists.");
            var user = new User();
            var passwordSaltAndHash = Password.CreatePasswordHash(request.Password);

            // update user properties
            user.UserName = request.UserName;
            user.PasswordSalt = passwordSaltAndHash.Item1;
            user.PasswordHash = passwordSaltAndHash.Item2;
            user.IsDeleted = false;
            // _currentUserService.UserName is null as API action to create user is anonymous.
            //user.CreatedBy = _currentUserService.UserName;
            user.CreatedBy = "anonymous";
            var userResponse = _mapper.Map<UserResponse>(await _userRepository.AddAsync(user));
            _logger.LogInformation($"Created user, {JsonSerializer.Serialize(userResponse)}.");

            return userResponse != null;
        }
    }
}