using Pedidos.Domain.Exceptions;
using Pedidos.Domain.Expressions;

namespace Pedidos.Domain.ValueObjects;

public record EmailAddress
{
    public EmailAddress(string address)
    {
        DomainExceptionValidation.When<InvalidEmailArgumentException>(IsValidEmail(address) is false);
        Address = address;
    }

    public string Address { get; } = null!;

    public override int GetHashCode()
    {
        return Address.GetHashCode();
    }

    public override string? ToString()
    {
        return Address;
    }

    private static bool IsValidEmail(string email)
    {
        return string.IsNullOrEmpty(email) is false && Expression.ValidEmail().IsMatch(email);
    }

    public static bool TryCreate(string email, out EmailAddress result)
    {
        result = null!;

        if (IsValidEmail(email))
        {
            result = new EmailAddress(email);
            return true;
        }

        return false;
    }

    public static implicit operator string(EmailAddress email)
    {
        return email.Address;
    }
}