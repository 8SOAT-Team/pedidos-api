﻿using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Pedidos.Domain.Clientes.Entities;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Exceptions;
using Pedidos.Domain.Pedidos.DomainEvents;
using Pedidos.Domain.Pedidos.Enums;
using Pedidos.Domain.Pedidos.ValueObjects;

namespace Pedidos.Domain.Pedidos.Entities;

public class Pedido : Entity, IAggregateRoot
{
    private const StatusPedido StatusInicial = StatusPedido.Recebido;
    private const StatusPedido StatusFinal = StatusPedido.Finalizado;

    private static readonly ImmutableHashSet<StatusPedido> StatusPedidoPermiteAlteracao =
        [StatusPedido.Recebido, StatusPedido.EmPreparacao];

    protected Pedido()
    {
    }

    [JsonConstructor]
    public Pedido(Guid id, Guid? clienteId, List<ItemDoPedido> itensDoPedido)
    {
        ValidationDomain(id, clienteId, itensDoPedido);

        Id = id;
        ClienteId = clienteId;
        ItensDoPedido = itensDoPedido;
        DataPedido = DateTime.Now;
        StatusPedido = StatusInicial;
        CalcularValorTotal();
    }

    public DateTime DataPedido { get; private set; }
    public StatusPedido StatusPedido { get; private set; }
    public Guid? ClienteId { get; private set; }
    public virtual Cliente? Cliente { get; set; }
    public virtual ICollection<ItemDoPedido> ItensDoPedido { get; set; }
    public decimal ValorTotal { get; private set; }
    public virtual Pagamento? Pagamento { get; set; }

    public void CalcularValorTotal()
    {
        ValorTotal = ItensDoPedido.Sum(item => item.Produto.Preco * item.Quantidade);
    }

    private static void ValidationDomain(Guid id, Guid? clienteId, List<ItemDoPedido> itens)
    {
        DomainExceptionValidation.When(id == Guid.Empty, "Id inválido");
        DomainExceptionValidation.When(clienteId == Guid.Empty, "Informar um id de cliente válido é obrigatório");
        DomainExceptionValidation.When(itens.Count <= 0, "O pedido deve conter pelo menos um item");
    }

    public void IniciarPreparo(StatusPagamento statusPagamento)
    {
        DomainExceptionValidation.When(Pagamento is null, $"Não é possível iniciar o preparo sem um {nameof(Pagamento)} iniciado.");
        
        DomainExceptionValidation.When(statusPagamento is not StatusPagamento.Autorizado,
            $"O pagamento deve estar {StatusPagamento.Autorizado} para iniciar o preparo.");

        DomainExceptionValidation.When(StatusPedido != StatusPedido.Recebido,
            $"Status do pedido não permite iniciar preparo. O status deve ser {StatusPedido.Recebido} para iniciar o preparo.");

        Pagamento!.AtualizarStatus(statusPagamento);

        if (Pagamento.EstaAutorizado() is false)
        {
            return;
        }
        
        StatusPedido = StatusPedido.EmPreparacao;
        
        RaiseEvent(new PedidoEmPreparacao(Id));
    }

    public Pedido FinalizarPreparo()
    {
        DomainExceptionValidation.When(StatusPedido != StatusPedido.EmPreparacao,
            $"Status do pedido não permite finalizar o preparo. O status deve ser {StatusPedido.EmPreparacao} para finalizar o preparo.");

        StatusPedido = StatusPedido.Pronto;
        RaiseEvent(new PedidoPronto(Id));
        return this;
    }

    public Pedido Entregar()
    {
        DomainExceptionValidation.When(StatusPedido != StatusPedido.Pronto,
            $"O pedido deve estar {StatusPedido.Pronto} para realizar a entrega.");

        StatusPedido = StatusFinal;

        RaiseEvent(new PedidoFinalizado(Id));
        return this;
    }

    public Pedido Cancelar()
    {
        DomainExceptionValidation.When(StatusPedido == StatusFinal,
            $"O pedido não pode ser cancelado após ser {StatusFinal}.");

        StatusPedido = StatusPedido.Cancelado;

        RaiseEvent(new PedidoCancelado(Id));
        return this;
    }

    public void ConfirmarPedido(MetodoDePagamento metodoDePagamento)
    {
        DomainExceptionValidation.When(StatusPedido != StatusInicial,
            $"Status do pedido não permite confirmação. O status deve ser {StatusPedido.Recebido} para realizar a confirmação.");

        RaiseEvent(new PedidoConfirmado(Id,metodoDePagamento, Cliente!, ItensDoPedido.ToList()));
    }

    public void PagamentoCriado(Guid pagamentoId, string urlPagamento)
    {
        Pagamento = new Pagamento
        {
            Id = pagamentoId,
            Status = StatusPagamento.Pendente,
            UrlPagamento = urlPagamento
        };
    }
}