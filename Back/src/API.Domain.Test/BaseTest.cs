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

    public class TestMap 
    {
        ProductController productController;
        Mock<IProductService> _productServiceMock;
        TodoMockData _todoMockData;
        int StatesOkResponse;

        public TestMap()
        {
            StatesOkResponse = 200;
            _todoMockData = new TodoMockData();
            _productServiceMock = new Mock<IProductService>();

            foreach (Produto produto in _todoMockData.GetTodos())
            {
                _productServiceMock.Setup(p => p.AddProduct(It.IsAny<Produto>())).Returns(Task.FromResult(produto));
                _productServiceMock.Setup(p => p.GetProductById(It.IsAny<int>())).Returns(Task.FromResult(produto));
                _productServiceMock.Setup(p => p.HistoricProduct(It.IsAny<int>())).Returns(Task.FromResult(produto));
                _productServiceMock.Setup(p => p.UpdateProduct(It.IsAny<int>(),It.IsAny<Produto>())).Returns(Task.FromResult(produto));
                _productServiceMock.Setup(p => p.DeleteProduct(It.IsAny<int>())).Returns(Task.FromResult(true));
            }
            productController = new ProductController(_productServiceMock.Object);
        }

        [Fact (DisplayName ="Devera retornar todos os produtos e  o statusCode OK (200)")]
        public async Task TestGetAllProducts()
        {
            var result = (OkObjectResult)await productController.GetProducts();

            Assert.Equal(StatesOkResponse, result.StatusCode);

        }
        [Theory(DisplayName = "GetProductById Deverá retornar o produto do Id referido e o  status code 200(OK)")]
        [InlineData(3)]
        public async Task TestGetProductById(int productId)
        {
            var result = (OkObjectResult)await productController.GetProductById(productId);

            Assert.Equal(StatesOkResponse, result.StatusCode);
        }
        
        [Theory(DisplayName = "Devera deletar o produto e retornar um statusCode 200(OK)")]
        [InlineData(2)]
        public async Task TestDeleteProductApiOk(int idProduct)
        {

            var result = (OkObjectResult)await productController.DeleteProduct(idProduct, _todoMockData.GetTodos()[0]);

            Assert.Equal(StatesOkResponse, result.StatusCode);

        }

        [Theory(DisplayName = "Devera atualizar o produto e retornar um statusCode 200(OK)")]
        [InlineData(2)]
        public async Task TestUpdateProductApiOk(int idProduct)
        {

            var result = (OkObjectResult)await productController.AttProduct(idProduct, _todoMockData.GetTodos()[0]);

            Assert.Equal(StatesOkResponse, result.StatusCode);

        }

        [Fact(DisplayName = "Devera adicionar o produto e retornar um statusCode 200(OK)")]
        public async Task TestAddProductApiOk()
        {
            var result = (OkObjectResult)await productController.AddProduct(_todoMockData.GetTodos()[0]);

            Assert.Equal(StatesOkResponse, result.StatusCode);

        }

        [Theory(DisplayName = "Devera historizar o produto e retornar um statusCode 200(OK)")]
        [InlineData(2)]
        public async Task TestHistoricProductApiOk(int idProduct)
        {

            var result = (OkObjectResult)await productController.HistoricProductApi(idProduct);

            Assert.Equal(StatesOkResponse, result.StatusCode);

        }
    }
}
