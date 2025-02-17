﻿using Pedidos.Domain.Exceptions;
using Pedidos.Domain.Expressions;

namespace Pedidos.Domain.ValueObjects;

public record Cpf
{
    private string? _sanitizedValue;

    public Cpf(string value)
    {
        VerifyValueConstraints(value);
        Value = value;
    }

    public string Value { get; } = null!;

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string? ToString()
    {
        return Value;
    }

    private static void VerifyValueConstraints(string cpf)
    {
        DomainExceptionValidation.When(string.IsNullOrWhiteSpace(cpf),
            () => InvalidArgumentException.WithErrorMessage("Cpf é obrigatório."));
        DomainExceptionValidation.When(Expression.HasCpfLength().IsMatch(cpf) is false,
            () => InvalidArgumentException.WithErrorMessage("Cpf deve conter 11 dígitos."));
    }

    public string GetSanitized()
    {
        _sanitizedValue ??= Expression.DigitsOnly().Replace(Value, "");
        return _sanitizedValue!;
    }

    public static bool TryCreate(string document, out Cpf cpf)
    {
        cpf = null!;

        if (string.IsNullOrWhiteSpace(document) || Expression.HasCpfLength().IsMatch(document) is false) return false;

        cpf = new Cpf(document);
        return true;
    }

    public static implicit operator string(Cpf cpf)
    {
        return cpf.Value;
    }
}