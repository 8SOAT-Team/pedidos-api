using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pedidos.Apps.Produtos.Enums;
using Pedidos.Apps.Produtos.Gateways.Produtos;
using Pedidos.Domain.Produtos.Entities;
using Pedidos.Infrastructure.Databases;

namespace Pedidos.Infrastructure.Produtos.Gateways;

public class ProdutoGateway : IProdutoGateway
{
    private readonly FastOrderContext _dbContext;
    private readonly DbSet<Produto> _produtos;

    public ProdutoGateway(FastOrderContext dbContext)
    {
        _dbContext = dbContext;
        _produtos = dbContext.Set<Produto>();
    }

    public async Task<Produto> CreateProdutoAsync(Produto produto)
    {
        var insertedProduto = await _produtos.AddAsync(produto);
        await _dbContext.SaveChangesAsync();
        return insertedProduto.Entity;
    }

    public Task<Produto?> GetProdutoByIdAsync(Guid id)
    {
        const string query = "SELECT * FROM Produtos WHERE id = @id";
        return _produtos.FromSqlRaw(query, new SqlParameter("id", id))
            .FirstOrDefaultAsync();
    }


    public Task<Produto?> GetProdutoCompletoByIdAsync(Guid id)
    {
        return _dbContext.Set<Produto>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Produto>> GetProdutosByCategoriaAsync(ProdutoCategoria categoria)
    {
        var domainCategoria = (Domain.Produtos.Enums.ProdutoCategoria)categoria;
        return await _dbContext.Set<Produto>()
            .Where(x => x.Categoria == domainCategoria)
            .ToListAsync();
    }

    public async Task<ICollection<Produto>> ListarProdutosByIdAsync(ICollection<Guid> ids)
    {
        return await _dbContext.Set<Produto>()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<ICollection<Produto>> ListarTodosProdutosAsync()
    {
        return await _dbContext.Set<Produto>().ToListAsync();
    }
}