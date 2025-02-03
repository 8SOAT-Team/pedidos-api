using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.Domain.Produtos.Entities;

namespace Pedidos.Infrastructure.Databases.EntityMapping;

internal class ProdutoTypeConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produtos");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome).IsRequired().HasMaxLength(100).IsRequired();
        builder.Property(p => p.Descricao).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Categoria);
        builder.Property(p => p.Preco).HasPrecision(18, 2);
        builder.Property(p => p.Imagem).IsRequired().HasMaxLength(300);
    }
}