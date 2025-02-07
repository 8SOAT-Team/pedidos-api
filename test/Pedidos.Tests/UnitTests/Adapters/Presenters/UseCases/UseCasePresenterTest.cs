using CleanArch.UseCase.Faults;
using Pedidos.Adapters.Presenters.UseCases;

namespace Pedidos.Tests.UnitTests.Adapters.Presenters.UseCases;

public sealed class UseCasePresenterTest
{
    [Fact]
    public void Should_Create_Success_Response()
    {
        // Arrange
        var useCaseError = new UseCaseError(UseCaseErrorType.BadRequest, "description");

        // Act
        var response = useCaseError.AdaptUseCaseError();

        // Assert
        Assert.Equal("Erro ao executar caso de uso", response.Title);
        Assert.Equal("description", response.Detail);
        Assert.Equal(UseCaseErrorType.BadRequest.ToString(), response.Status);
        Assert.Equal("description", response.Detail);
        Assert.Equal(null, response.Instance);
    }
    
    [Fact]
    public void Should_Create_Failure_Response()
    {
        // Arrange
        var useCaseErrors = new List<UseCaseError>
        {
            new UseCaseError(UseCaseErrorType.BadRequest, "description")
        };

        // Act
        var response = useCaseErrors.AdaptUseCaseErrors();

        // Assert
        Assert.Single(response);
        Assert.Equal("Erro ao executar caso de uso", response.First().Title);
        Assert.Equal("description", response.First().Detail);
        Assert.Equal(UseCaseErrorType.BadRequest.ToString(), response.First().Status);
        Assert.Equal("description", response.First().Detail);
        Assert.Equal(null, response.First().Instance);
    }
    
    [Fact]
    public void Should_Create_Success_Response_With_EntityId()
    {
        // Arrange
        var useCaseError = new UseCaseError(UseCaseErrorType.BadRequest, "description");

        // Act
        var response = useCaseError.AdaptUseCaseError(entityId: "entityId");

        // Assert
        Assert.Equal("Erro ao executar caso de uso", response.Title);
        Assert.Equal("description", response.Detail);
        Assert.Equal(UseCaseErrorType.BadRequest.ToString(), response.Status);
        Assert.Equal("description", response.Detail);
        Assert.Equal("entityId", response.Instance);
    }

    [Fact]
    public void Should_Create_Success_Response_List()
    {
        // Arrange
        var useCaseErrors = new List<UseCaseError>
        {
            new UseCaseError(UseCaseErrorType.BadRequest, "description")
        };
        
        // Act
        var response = useCaseErrors.AdaptUseCaseErrors(entityId: "entityId");
        
        // Assert
        Assert.Single(response);
    }
}