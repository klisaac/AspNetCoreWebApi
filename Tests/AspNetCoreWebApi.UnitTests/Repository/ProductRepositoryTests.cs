using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using AspNetCoreWebApi.Infrastructure.Data;
using AspNetCoreWebApi.Infrastructure.Repository;
using AspNetCoreWebApi.UnitTests.Builders;

namespace AspNetCoreWebApi.UnitTests.Repository
{
    public class ProductRepositoryTests
    {
        private readonly AppDataContext _appDataContext;
        private readonly ProductRepository _productRepository;
        private readonly ITestOutputHelper _output;
        private ProductBuilder ProductBuilder { get; } = new ProductBuilder();

        public ProductRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<AppDataContext>()
                .UseInMemoryDatabase(databaseName: "AspNetCoreWebApiTest")
                .Options;
            _appDataContext = new AppDataContext(dbOptions);
            AppDataSeed.SeedAsync(_appDataContext, false).Wait();
            _productRepository = new ProductRepository(_appDataContext);
        }

        [Fact]
        public async Task Test_Create_New_Product()
        {
            var newProduct = ProductBuilder.NewProductValues();
            _output.WriteLine($"New Product: {newProduct.Name}");

            var productFromRepo = await _productRepository.AddAsync(newProduct);
            Assert.True(productFromRepo.ProductId > 0);
            Assert.Equal(productFromRepo.Code, productFromRepo.Code);
            Assert.Equal(productFromRepo.Name, productFromRepo.Name);
            Assert.Equal(productFromRepo.Summary, productFromRepo.Summary);
            Assert.Equal(productFromRepo.UnitPrice, productFromRepo.UnitPrice);
            Assert.Equal(productFromRepo.UnitsInStock, productFromRepo.UnitsInStock);
        }

        [Fact]
        public async Task Test_Get_Existing_Product()
        {
            var product = _appDataContext.Products.First();
            var productId = product.ProductId;
            _output.WriteLine($"ProductId: {productId}");

            var productFromRepo = await _productRepository.GetByIdAsync(productId);
            Assert.Equal(product.ProductId, productFromRepo.ProductId);
            Assert.Equal(product.Code, productFromRepo.Code);
            Assert.Equal(product.Name, productFromRepo.Name);
            Assert.Equal(productFromRepo.Summary, productFromRepo.Summary);
            Assert.Equal(productFromRepo.UnitPrice, productFromRepo.UnitPrice);
            Assert.Equal(productFromRepo.UnitsInStock, productFromRepo.UnitsInStock);
        }

        [Fact]
        public async Task Test_Get_Product_By_Id()
        {
            var product = _appDataContext.Products.First();
            var productId = product.ProductId;
            _output.WriteLine($"ProductId: {productId}");

            var productFromRepo = await _productRepository.GetByIdAsync(productId);
            Assert.NotNull(productFromRepo);

            Assert.Equal(product.ProductId, productFromRepo.ProductId);
            Assert.Equal(product.Code, productFromRepo.Code);
            Assert.Equal(product.Name, productFromRepo.Name);
            Assert.Equal(productFromRepo.Summary, productFromRepo.Summary);
            Assert.Equal(productFromRepo.UnitPrice, productFromRepo.UnitPrice);
            Assert.Equal(productFromRepo.UnitsInStock, productFromRepo.UnitsInStock);
        }
    }
}
