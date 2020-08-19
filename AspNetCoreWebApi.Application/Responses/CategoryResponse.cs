using AspNetCoreWebApi.Application.Models;

namespace AspNetCoreWebApi.Application.Responses
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
