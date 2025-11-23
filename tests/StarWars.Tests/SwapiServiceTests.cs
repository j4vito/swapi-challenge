using System.Net;
using Moq;
using Moq.Protected;
using StarWars.Api.Services;
using Xunit;

namespace StarWars.Tests;

public class SwapiServiceTests
{
    [Fact]
    public async Task GetPersonAsync_ReturnsPerson_WhenApiReturnsSuccess()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{\"name\": \"Luke Skywalker\", \"height\": \"172\"}"),
        };

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(response);

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://swapi.dev/api/")
        };

        var service = new SwapiService(httpClient);

        // Act
        var result = await service.GetPersonAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Luke Skywalker", result.Name);
    }

    [Fact]
    public async Task GetPersonAsync_ReturnsNull_WhenApiReturnsNotFound()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound,
        };

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(response);

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://swapi.dev/api/")
        };

        var service = new SwapiService(httpClient);

        // Act
        var result = await service.GetPersonAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPlanetAsync_ReturnsPlanet_WhenApiReturnsSuccess()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>();
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("{\"name\": \"Tatooine\", \"climate\": \"arid\"}"),
        };

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(response);

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://swapi.dev/api/")
        };

        var service = new SwapiService(httpClient);

        // Act
        var result = await service.GetPlanetAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Tatooine", result.Name);
    }
}
