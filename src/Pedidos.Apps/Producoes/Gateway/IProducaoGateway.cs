using Pedidos.Domain.Pedidos.Entities;

namespace Pedidos.Apps.Producoes.Gateway;
public interface IProducaoGateway
{
    Task<Pedido> IniciarProducaoAsync(Guid pedidoId);
}