﻿using Pedidos.Adapters.Controllers.Pedidos.Dtos;
using Pedidos.Adapters.Controllers.Pedidos.Enums;
using Pedidos.Domain.Pedidos.Entities;
using Pedidos.Domain.Pedidos.Enums;

namespace Pedidos.Adapters.Presenters.Pedidos;

public static class PedidoPresenter
{
    public static PedidoDto ToPedidoDto(this Pedido pedido)
    {
        return new PedidoDto
        {
            Id = pedido.Id,
            DataPedido = pedido.DataPedido,
            StatusPedido = pedido.StatusPedido,
            // Cliente = pedido.Cliente is null ? null : ClientePresenter.AdaptCliente(pedido.Cliente!),
            ItensDoPedido = pedido.ItensDoPedido.Select(p => new ItemDoPedidoDto
            {
                Id = p.Id,
                ProdutoId = p.ProdutoId,
                Quantidade = p.Quantidade,
                Imagem = p.Produto?.Imagem!
            }).ToList(),
            ValorTotal = pedido.ValorTotal
            // Pagamento = pedido.Pagamento is null ? null : PagamentoPresenter.ToPagamentoDTO(pedido.Pagamento)
        };
    }

    public static List<PedidoDto> ToListPedidoDto(this List<Pedido> pedidos)
    {
        return pedidos.Select(p => p.ToPedidoDto()).ToList();
    }

    public static PedidoConfirmadoDto ToPedidoConfirmadoDto(this Pedido pedido) => new(pedido.Id,
        pedido.Pagamento?.UrlPagamento!, Enum.Parse<StatusDoPagamento>(pedido.Pagamento?.Status.ToString() ??
                                                                       StatusPagamento.Pendente.ToString()));
}