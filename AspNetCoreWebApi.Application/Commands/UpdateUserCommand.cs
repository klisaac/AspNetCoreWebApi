using MediatR;

namespace AspNetCoreWebApi.Application.Commands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
