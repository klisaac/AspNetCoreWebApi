using MediatR;

namespace AspNetCoreWebApi.Application.Commands
{
    public class LoginUserCommand: IRequest<bool>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
