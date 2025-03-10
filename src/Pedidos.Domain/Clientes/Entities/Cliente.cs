﻿using System.Text.Json.Serialization;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Exceptions;
using Pedidos.Domain.ValueObjects;

namespace Pedidos.Domain.Clientes.Entities;

public class Cliente : Entity, IAggregateRoot
{
    protected Cliente()
    {
    }

    public Cliente(string cpf, string nome, string email) : this(Guid.NewGuid(), cpf, nome, email)
    {
    }

    public Cliente(Guid id, string cpf, string nome, string email) : this(id, new Cpf(cpf), nome,
        new EmailAddress(email))
    {
    }

    [JsonConstructor]
    public Cliente(Guid id, Cpf cpf, string nome, EmailAddress email)
    {
        DomainExceptionValidation.When(id == Guid.Empty, "Id inválido");
        ValidationDomain(nome);

        Id = id;
        Cpf = cpf;
        Nome = nome;
        Email = email;
    }

    public virtual Cpf Cpf { get; private init; } = null!;
    public string Nome { get; private set; } = null!;
    public virtual EmailAddress Email { get; private set; } = null!;

    public void ChangeNome(string nome)
    {
        ValidationDomain(nome);
        Nome = nome;
    }

    public void ChangeEmail(EmailAddress email)
    {
        Email = email;
    }

    private void ValidationDomain(string nome)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome é obrigatório");

        DomainExceptionValidation.When(nome!.Length < 3, "Nome deve ter no mínimo 3 caracteres");

        DomainExceptionValidation.When(nome.Length > 100, "Nome deve ter no máximo 100 caracteres");
    }
}