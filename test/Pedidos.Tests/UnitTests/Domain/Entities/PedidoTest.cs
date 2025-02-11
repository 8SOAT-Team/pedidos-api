using Pedidos.Domain.Exceptions;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Produtos.Entities;
using Pedidos.Domain.Produtos.Enums;

namespace Pedidos.Tests.UnitTests.Domain.Entities
{
    public class PedidoTest
    {
        [Fact]
        public void DeveCriarNovoPedidoComSucesso()
        {
            //Arrange
            var produto = new Produto("Lanche", "Lanche de bacon", 50m, "http://endereco/imagens/img.jpg", ProdutoCategoria.Acompanhamento);
            var itemPedido = new ItemDoPedido(Guid.NewGuid(), produto, 2);
            List<ItemDoPedido> listaItens = new List<ItemDoPedido>();
            listaItens.Add(itemPedido);
            //Act
            var pedido = new Pedido(Guid.NewGuid(), Guid.NewGuid(), listaItens);
            //Assert
            Assert.NotNull(pedido);
        }

        [Fact]
        public void DeveLancarExceptionQuandoPedidoNaoTiverItens()
        {
            //Arrange
            List<ItemDoPedido> listaItens = new List<ItemDoPedido>();
            //Act
            Action act = () => new Pedido(Guid.NewGuid(), Guid.NewGuid(), listaItens);
            //Assert
            Assert.Throws<DomainExceptionValidation>(() => act());
        }

        [Fact]
        public void DeveLancarExceptionQuandoIdPedidoInvalido()
        {
            //Arrange
            var itemPedido = new ItemDoPedido(Guid.NewGuid(), Guid.NewGuid(), 2);
            List<ItemDoPedido> listaItens = new List<ItemDoPedido>();
            listaItens.Add(itemPedido);
            //Act
            Action act = () => new Pedido(Guid.Empty, Guid.NewGuid(), listaItens);
            //Assert
            Assert.Throws<DomainExceptionValidation>(() => act());
        }

        [Fact]
        public void DeveLancarExceptionQuandoPedidoTiverIdClienteInvalido()
        {
            //Arrange
            var itemPedido = new ItemDoPedido(Guid.NewGuid(), Guid.NewGuid(), 2);
            List<ItemDoPedido> listaItens = new List<ItemDoPedido>();
            listaItens.Add(itemPedido);
            //Act
            Action act = () => new Pedido(Guid.NewGuid(), Guid.Empty, listaItens);
            //Assert
            Assert.Throws<DomainExceptionValidation>(() => act());
        }
    }
}
