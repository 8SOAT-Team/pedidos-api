using Pedidos.Adapters.Types.Results;

namespace Pedidos.Tests.UnitTests.Adapters.Types.Results;

public sealed class ResultTest
{
    [Fact]
    public void Should_Create_Success_Result()
    {
        // Arrange
        var value = new object();

        // Act
        var result = Result<object>.Succeed(value);

        // Assert
        Assert.True(result.HasValue);
        Assert.False(result.IsFailure);
        Assert.True(result.IsSucceed);
        Assert.Equal(value, result.Value);
    }
    
    [Fact]
    public void Should_Create_Failure_Result()
    {
        // Arrange
        var details = new List<AppProblemDetails>
        {
            new AppBadRequestProblemDetails("title", "detail", "instance")
        };

        // Act
        var result = Result<object>.Failure(details);

        // Assert
        Assert.False(result.HasValue);
        Assert.True(result.IsFailure);
        Assert.False(result.IsSucceed);
        Assert.Equal(details, result.ProblemDetails);
    }
    
    [Fact]
    public void Should_Create_Empty_Result()
    {
        // Act
        var result = Result<object>.Empty();

        // Assert
        Assert.False(result.HasValue);
        Assert.False(result.IsFailure);
        Assert.True(result.IsSucceed);
    }
    
    [Fact]
    public void Should_Call_Success_Action()
    {
        // Arrange
        var value = new object();
        var result = Result<object>.Succeed(value);
        var onSuccessCalled = false;

        // Act
        result.Match(
            onSuccess: _ => onSuccessCalled = true,
            onFailure: _ => { });

        // Assert
        Assert.True(onSuccessCalled);
    }
    
    [Fact]
    public void Should_Call_Failure_Action()
    {
        // Arrange
        var details = new List<AppProblemDetails>
        {
            new AppBadRequestProblemDetails("title", "detail", "instance")
        };
        var result = Result<object>.Failure(details);
        var onFailureCalled = false;

        // Act
        result.Match(
            onSuccess: _ => { },
            onFailure: _ => onFailureCalled = true);

        // Assert
        Assert.True(onFailureCalled);
    }
    
    [Fact]
    public void IsFailure_Should_Be_True_When_Has_ProblemDetails()
    {
        // Arrange
        var details = new List<AppProblemDetails>
        {
            new AppBadRequestProblemDetails("title", "detail", "instance")
        };
        var result = Result<object>.Failure(details);

        // Act
        var isFailure = result.IsFailure;

        // Assert
        Assert.True(isFailure);
    }
    
    [Fact]
    public void IsFailure_Should_Be_False_When_Has_No_ProblemDetails()
    {
        // Arrange
        var value = new object();
        var result = Result<object>.Succeed(value);

        // Act
        var isFailure = result.IsFailure;

        // Assert
        Assert.False(isFailure);
    }
    
    [Fact]
    public void IsSucceed_Should_Be_True_When_Has_Value()
    {
        // Arrange
        var value = new object();
        var result = Result<object>.Succeed(value);

        // Act
        var isSucceed = result.IsSucceed;

        // Assert
        Assert.True(isSucceed);
    }
    
    [Fact]
    public void IsSucceed_Should_Be_False_When_Has_No_Value()
    {
        // Arrange
        var details = new List<AppProblemDetails>
        {
            new AppBadRequestProblemDetails("title", "detail", "instance")
        };
        var result = Result<object>.Failure(details);

        // Act
        var isSucceed = result.IsSucceed;

        // Assert
        Assert.False(isSucceed);
    }
    
    [Fact]
    public void HasValue_Should_Be_True_When_Has_Value()
    {
        // Arrange
        var value = new object();
        var result = Result<object>.Succeed(value);

        // Act
        var hasValue = result.HasValue;

        // Assert
        Assert.True(hasValue);
    }
    
    [Fact]
    public void HasValue_Should_Be_False_When_Has_No_Value()
    {
        // Arrange
        var details = new List<AppProblemDetails>
        {
            new AppBadRequestProblemDetails("title", "detail", "instance")
        };
        var result = Result<object>.Failure(details);

        // Act
        var hasValue = result.HasValue;

        // Assert
        Assert.False(hasValue);
    }
}