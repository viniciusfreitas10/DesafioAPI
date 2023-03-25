using Xunit;
using Produtos.Application;
using Produtos.Domain;
using Produtos.Persistence;
using System;
using Microsoft.Extensions.DependencyInjection;
using Produtos.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Produtos.Application.Contratos;
using Produtos.Persistence.Contratos;
using System.Configuration;
using Produtos.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Produtos.API;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System.Net.Http;
using System.Net;

namespace API.Domain.Test
{
    //[Collection("ProductController")]
    //public class BaseTest : IClassFixture<WebApplicationFactory<Startup>>
    public class BaseTest 
        : IClassFixture<IProductService>
    {
       public BaseTest() { }
            
    }

    public class TestMap 
        //: IClassFixture<IProductService>
    {
        ProductController productController;
        Mock<IProductService> _productServiceMock;

        public TestMap()
        {
            _productServiceMock = new Mock<IProductService>();
            //_productServiceMock.Setup(x => x.GetAllProductAsync());
            productController = new ProductController(_productServiceMock.Object);
        }

        [Fact (DisplayName ="Devera retornar o statusCode OK (200)")]
        public async Task TestGetProducts()
        {
            int StatesOkResponse = 200;

            var result = productController.GetProducts();

            var OkObjectResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(StatesOkResponse, OkObjectResult.StatusCode);

        }

        [Fact]
        public void TestPar()
        {
            int um = 1;
            int dois = 2;
            int esperado = 3;

            int r = um + dois;

            Assert.Equal(esperado, r);
        }
    }
}
