using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using AutoMapper;
using MediatR;
using AspNetCoreWebApi.Application.Commands;
using AspNetCoreWebApi.Application.Common.Identity;
using AspNetCoreWebApi.Application.Common.Exceptions;
using AspNetCoreWebApi.Core.Logging;
using AspNetCoreWebApi.Core.Repository;
using AspNetCoreWebApi.Core.Specifications;
using AspNetCoreWebApi.Application.Common.Helpers;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWebApi.Application.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IAppLogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(IUserRepository userRepository, ICurrentUser currentUser, IMapper mapper, IAppLogger<UpdateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                throw new BadRequestException($"User with user, {request.UserId} does not exist.");

            // throw error if the new username is already taken
            if (await _userRepository.GetSingleAsync(new UserSpecification(request.UserName)) != null)
            {
                _logger.LogInformation($"User, {request.UserName} already exists.");
                throw new BadRequestException($"User, {request.UserName} already exists.");
            }
            var passwordSaltAndHash = Password.CreatePasswordHash(request.Password);

            // update user properties
            user.UserName = request.UserName;
            user.PasswordSalt = passwordSaltAndHash.Item1;
            user.PasswordHash = passwordSaltAndHash.Item2;
            user.LastModifiedBy = _currentUser.UserName;
            var userResponse = _mapper.Map<UserResponse> (await _userRepository.UpdateAsync(user));
            _logger.LogInformation($"Updated user, {JsonSerializer.Serialize(userResponse)}.");

            return userResponse != null;
        }
    }
}