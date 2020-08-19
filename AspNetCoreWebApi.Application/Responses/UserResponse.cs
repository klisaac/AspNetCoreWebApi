using System.ComponentModel.DataAnnotations;

namespace AspNetCoreWebApi.Application.Responses
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}