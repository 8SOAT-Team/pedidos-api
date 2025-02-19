using Pedidos.Apps.Produtos.Enums;
using Pedidos.Apps.Produtos.UseCases.DTOs;
using Pedidos.Apps.Types.Results;

namespace Pedidos.Adapters.Controllers.Produtos;

public interface IProdutoController
{
    Task<Result<ProdutoDto?>> GetProdutoByIdAsync(Guid id);
    Task<Result<ProdutoCriadoDto>> CreateProdutoAsync(NovoProdutoDto produto);
    Task<Result<ICollection<ProdutoDto>>> GetAllProdutosAsync();
    Task<Result<ICollection<ProdutoDto>>> ListarProdutoPorCategoriaAsync(ProdutoCategoria categoria);
}