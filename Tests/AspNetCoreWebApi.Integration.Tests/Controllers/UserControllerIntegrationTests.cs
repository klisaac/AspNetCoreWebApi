using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using AspNetCoreWebApi.Application.Commands;
using AspNetCoreWebApi.Integration.Tests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreWebApi.Infrastructure.Data;
using AspNetCoreWebApi.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace AspNetCoreWebApi.Integration.Tests.Controllers
{
    //[TestCaseOrderer("AspNetCoreWebApi.Integration.Tests.Helpers.PriorityOrderer", "AspNetCoreWebApi.Integration.Tests")]
    //[Collection("Sequential")]
    public class UserControllerIntegrationTests : BaseTest
    {
        public UserControllerIntegrationTests(ApplicationFactoryTest<StartupTest> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Test_CreateUser_WithValidUserDetails()
        {
            var provider = ClaimsProviderTest.WithAdminClaims();
            var client = Factory.CreateClientWithTokenAuth(provider);
            var httpResponse = await client.PostAsync("/api/user/create", new StringContent(JsonConvert.SerializeObject(new CreateUserCommand() { UserName = "adrian2", Password = "welcome", ConfirmPassword = "welcome" }), Encoding.UTF8, "application/json"));
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task Test_Login_WithValidCredentials()
        {
            var client = Factory.CreateClient();
            var httpResponse = await client.PostAsync("/api/user/authenticate", new StringContent(JsonConvert.SerializeObject(new LoginUserCommand() { UserName = "adrian2", Password = "welcome" }), Encoding.UTF8, "application/json"));
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            dynamic result = JObject.Parse(stringResponse);
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Test_UpdateUser_WithValidUserDetails()
        {
            var provider = ClaimsProviderTest.WithAdminClaims();
            var client = Factory.CreateClientWithTokenAuth(provider);
            var httpResponse = await client.PutAsync("/api/user/update", new StringContent(JsonConvert.SerializeObject(new UpdateUserCommand() { UserId = 1, UserName = "adrian2", Password = "Welcome123", ConfirmPassword = "Welcome123" }), Encoding.UTF8, "application/json"));
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.True(Convert.ToBoolean(stringResponse));
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}

 

