using Pedidos.Adapters.Types.Results;
using Pedidos.Apps.Produtos.Enums;
using Pedidos.Apps.Produtos.UseCases.DTOs;

namespace Pedidos.Adapters.Controllers.Produtos;

public interface IProdutoController
{
    Task<Result<ProdutoDto?>> GetProdutoByIdAsync(Guid id);
    Task<Result<ProdutoCriadoDto>> CreateProdutoAsync(NovoProdutoDto produto);
    Task<Result<ICollection<ProdutoDto>>> GetAllProdutosAsync();
    Task<Result<ICollection<ProdutoDto>>> ListarProdutoPorCategoriaAsync(ProdutoCategoria categoria);
}