namespace Pedidos.Domain.Exceptions;

public class InvalidEmailArgumentException : InvalidArgumentException
{
    private const string ErrorMessage = "Email não está em um formato válido.";

    public InvalidEmailArgumentException() : base(ErrorMessage)
    {
    }
}