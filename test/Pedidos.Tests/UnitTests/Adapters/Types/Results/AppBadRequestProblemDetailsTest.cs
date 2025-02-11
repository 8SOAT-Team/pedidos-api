using FluentAssertions;
using Pedidos.Adapters.Types.Results;

namespace Pedidos.Tests.UnitTests.Adapters.Types.Results;

public sealed class AppBadRequestProblemDetailsTest
{
    [Fact]
    public void Deve_Criar_Um_AppBadRequestProblemDetails()
    {
        // Arrange
        const string type = "type";
        const string detail = "detail";
        const string instance = "instance";

        // Act
        var appBadRequestProblemDetails = new AppBadRequestProblemDetails(type, detail, instance);

        // Assert
        appBadRequestProblemDetails.Type.Should().Be(type);
        appBadRequestProblemDetails.Title.Should().Be("Request invalido");
        appBadRequestProblemDetails.Status.Should().Be(StatusConst.BadRequest);
        appBadRequestProblemDetails.Detail.Should().Be(detail);
        appBadRequestProblemDetails.Instance.Should().Be(instance);
    }
    
    [Fact]
    public void Deve_Criar_Um_AppBadRequestProblemDetails_Com_Type_Vazio()
    {
        // Arrange
        const string detail = "detail";
        const string instance = "instance";

        // Act
        var appBadRequestProblemDetails = new AppBadRequestProblemDetails(detail, instance);

        // Assert
        appBadRequestProblemDetails.Type.Should().Be("");
        appBadRequestProblemDetails.Title.Should().Be("Request invalido");
        appBadRequestProblemDetails.Status.Should().Be(StatusConst.BadRequest);
        appBadRequestProblemDetails.Detail.Should().Be(detail);
        appBadRequestProblemDetails.Instance.Should().Be(instance);
    }
}