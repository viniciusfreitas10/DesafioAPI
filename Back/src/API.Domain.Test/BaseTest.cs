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
    public class TodoMockData
    {
        public DateTime dateTimeAtual = DateTime.Now;
        public DateTime dateTimeValidade = DateTime.Now.AddDays(1);
        public List<Produto> GetTodos()
        {

            return new List<Produto>{
                new Produto{
                    Id = 1,
                    Description = "Produto Mock teste",
                    EstReg = 'A',
                    DataFabricacao = dateTimeAtual,
                    DataValidade = dateTimeValidade,
                    CodigoFornecedor = 1,
                    DescricaoFornecedor = "Descrição mock teste",
                    Cnpj = "1234567090/0001"
                },
                new Produto{
                    Id = 2,
                    Description = "Produto Mock teste 2",
                    EstReg = 'A',
                    DataFabricacao = dateTimeAtual,
                    DataValidade = dateTimeValidade,
                    CodigoFornecedor = 1,
                    DescricaoFornecedor = "Descrição mock teste 2",
                    Cnpj = "1234567090/0001"
                },
                new Produto{
                    Id = 3,
                    Description = "Produto Mock teste 3",
                    EstReg = 'H',
                    DataFabricacao = dateTimeAtual,
                    DataValidade = dateTimeValidade,
                    CodigoFornecedor = 2,
                    DescricaoFornecedor = "Descrição mock teste 3",
                    Cnpj = "1234567090/0001"
                }

         };
        }
    }
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
            _productServiceMock.Setup(x => x.GetProductById(2));
            productController = new ProductController(_productServiceMock.Object);
        }

        [Fact (DisplayName ="Devera retornar o statusCode OK (200)")]
        public async Task TestGetAllProducts()
        {
            int StatesOkResponse = 200;

            var result = productController.GetProducts();

            var OkObjectResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(StatesOkResponse, OkObjectResult.StatusCode);

        }
        [Theory(DisplayName = "Devera retornar o produto e retornar um statusCode 200(OK)")]
        [InlineData(3)]
        public async Task TestGetProductById(int productId)
        {
            int StatesOkResponse = 200;

            var result = await productController.GetProductById(productId);

            var okResult = result as StatusCodeResult;

            ObjectResult objectResponse = Assert.IsType<ObjectResult>(okResult);

            Assert.Equal(StatesOkResponse, objectResponse.StatusCode);

        }
        [Theory(DisplayName = "Devera historizar o produto e retornar um statusCode 200(OK)")]
        [InlineData(2)]
        public async Task TestHistoricProductApi(int idProduct)
        {
            //Desse jeito funciona, porém o erro agora é pq não consegue recupar o Produto com o Id
            int StatesOkResponse = 200;

            var result = await productController.HistoricProductApi(idProduct);

            var okResult = result as StatusCodeResult;

            ObjectResult objectResponse = Assert.IsType<ObjectResult>(okResult);

            Assert.Equal(StatesOkResponse, objectResponse.StatusCode);


            //var OkObjectResult = Assert.IsType<OkObjectResult>(result.Result);
            //Assert.Equal(StatesOkResponse, OkObjectResult.StatusCode);
            //var okResult = result as StatusCodeResult;
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
    public class TestGetProductById
    {
        public TestGetProductById()
        {

        }

        [Theory(DisplayName = "Devera retornar o produto e retornar um statusCode 200(OK)")]
        [InlineData(3)]
        public async Task TestGetProductByI(int productId)
        {
            int StatesOkResponse = 200;
            // var result = await productController.GetProductById(productId);

            //var okResult = result as StatusCodeResult;

            //ObjectResult objectResponse = Assert.IsType<ObjectResult>(okResult);

            //Assert.Equal(StatesOkResponse, objectResponse.StatusCode);


            //daq pra baixo - teste novo

            /// Arrange
            //todoService.Setup(_ => _.GetProductById(productId)).ReturnsAsync(todoMockData.GetTodos());
            //todoService.Setup(_ => _.GetProductById(productId));

            TodoMockData todoMockData = new TodoMockData();
            var todoService = new Mock<IProductService>();
            /*
            foreach (Produto produto in todoMockData.GetTodos())
            {
            }*/

            //todoService.Setup(p => p.AddProduct(It.IsAny<Produto>())).Returns(new Task<Produto>);

            //todoService.Setup(p => p.GetProductById(It.IsAny<int>())).Returns(Task<Produto>.CompletedTask);

            todoService.Setup(p => p.AddProduct(It.IsAny<Produto>())).Returns(Task<Produto>.FromResult(default(Produto)));

            todoService.Setup(p => p.GetProductById(It.IsAny<int>())).Returns(Task<Produto>.FromResult(default(Produto)));

            //todoService.Setup(p => p.GetProductById(It.IsAny<int>())).Returns(Task<Produto>.CompletedTask);

            var sut = new ProductController(todoService.Object);

            var result = (OkObjectResult)await sut.GetProductById(productId);

            //Assert
            Assert.Equal(StatesOkResponse, result.StatusCode);
        }

    }
}
