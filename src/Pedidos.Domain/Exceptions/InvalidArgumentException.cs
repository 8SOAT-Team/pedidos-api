namespace Pedidos.Domain.Exceptions;

public class InvalidArgumentException : DomainExceptionValidation
{
    private const string ErrorMessage = "Parâmetro informado é inválido. (Parâmetro {0})";

    protected InvalidArgumentException(string error) : base(error) { }

    public static InvalidArgumentException InvalidParameter(string parameter) => new(string.Format(ErrorMessage, parameter));
    public static InvalidArgumentException WithErrorMessage(string error) => new(error);
}
