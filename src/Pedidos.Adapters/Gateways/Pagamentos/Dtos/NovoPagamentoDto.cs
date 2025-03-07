﻿using System.Diagnostics.CodeAnalysis;
using Pedidos.Adapters.Gateways.Pagamentos.Enums;

namespace Pedidos.Adapters.Gateways.Pagamentos.Dtos;
[ExcludeFromCodeCoverage]
public record NovoPagamentoDto
{
    public Guid PedidoId { get; init; }
    public MetodosDePagamento MetodoDePagamento { get; init; }

    public List<NovoPagamentoItemRequest> Itens { get; init; } = [];

    public NovoPagamentoPagadorRequest? Pagador { get; init; }
};

public record NovoPagamentoItemRequest
{
    public Guid Id { get; init; }
    public string Titulo { get; init; }
    public string Descricao { get; init; }
    public int Quantidade { get; init; }
    public decimal PrecoUnitario { get; init; }
}

public record NovoPagamentoPagadorRequest
{
    public string Email { get; init; }
    public string Nome { get; init; }
    public string Cpf { get; init; }
}