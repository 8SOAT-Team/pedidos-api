using Pedidos.Apps.Produtos.Enums;
using Pedidos.Domain.Produtos.Entities;

namespace Pedidos.Apps.Produtos.Gateways.Produtos;

public interface IProdutoGateway
{
    Task<Produto> CreateProdutoAsync(Produto produto);
    Task<Produto?> GetProdutoByIdAsync(Guid id);
    Task<Produto?> GetProdutoCompletoByIdAsync(Guid id);
    Task<ICollection<Produto>> GetProdutosByCategoriaAsync(ProdutoCategoria categoria);
    Task<ICollection<Produto>> ListarTodosProdutosAsync();
    Task<ICollection<Produto>> ListarProdutosByIdAsync(ICollection<Guid> ids);
}