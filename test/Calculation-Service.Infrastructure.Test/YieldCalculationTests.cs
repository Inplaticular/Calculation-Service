using FluentAssertions;
using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Contracts.V1.Responses;
using Inplanticular.CalculationService.Core.Options;
using Inplanticular.CalculationService.Infrastructure.Services;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Calculation_Service.Infrastructure.Test;

public class YieldCalculationTests {
	[Fact]
	public void CalcRequestAsync_ShouldReturnResponseWithExpectedYield_WhenWatered() {
		var yieldCalcRequest = new YieldCalcRequest {
			PlantCoordinateLatitude = 10000.0,
			PlantCoordinateLongitude = 10000.0,
			FertilizerPercentage = 0,
			ActFruitCount = 12.0,
			AvgFruitWeight = 23.0,
			DaysWithoutWater = 0
		};
		var options = new Mock<IOptions<WeatherAPIOptions>>();
		options.SetupGet(o => o.Value).Returns(new WeatherAPIOptions {
			Routes = new Routes {WeatherRoute = "https://api.openweathermap.org/data/2.5/weather"},
			APICredentials = new APICredentials {WeatherAPIKey = "ac54d952a883e860998804ec69b90937"}
		});
		var yieldCalcService = new YieldCalcService(options.Object);
		var yieldCalcResponse = yieldCalcService.CalcRequestAsync(yieldCalcRequest).Result;

		yieldCalcResponse.Succeeded.Should().BeTrue();
		yieldCalcResponse.Errors.Should().BeEmpty();
		yieldCalcResponse.Messages.Should().Contain(YieldCalcResponse.Message.YieldCalculationSuccessful);
		yieldCalcResponse.Yield.Should().Be(331.2);
	}

	[Fact]
	public void CalcRequestAsync_ShouldReturnResponse_WhenNotWateredButNotDead() {
		var yieldCalcRequest = new YieldCalcRequest {
			PlantCoordinateLatitude = 10000.0,
			PlantCoordinateLongitude = 10000.0,
			FertilizerPercentage = 0,
			ActFruitCount = 12.0,
			AvgFruitWeight = 23.0,
			DaysWithoutWater = 3
		};

		var options = new Mock<IOptions<WeatherAPIOptions>>();
		options.SetupGet(o => o.Value).Returns(new WeatherAPIOptions {
			Routes = new Routes {WeatherRoute = "https://api.openweathermap.org/data/2.5/weather"},
			APICredentials = new APICredentials {WeatherAPIKey = "ac54d952a883e860998804ec69b90937"}
		});
		var yieldCalcService = new YieldCalcService(options.Object);
		var yieldCalcResponse = yieldCalcService.CalcRequestAsync(yieldCalcRequest).Result;

		yieldCalcResponse.Yield.Should().Be(276,
			" weather could not have increased the yield, due to the fact, that" +
			yieldCalcRequest.PlantCoordinateLatitude + " and " + yieldCalcRequest.PlantCoordinateLongitude +
			" are not coordinate for a location on earth, so the api does not return any rain.");
	}

	[Fact]
	public void CalcRequestAsync_ShouldntReturnResponseWithNegativeYield_WhenNotWatered7Days() {
		var yieldCalcRequest = new YieldCalcRequest {
			PlantCoordinateLatitude = -34.4251,
			PlantCoordinateLongitude = 50.8931,
			FertilizerPercentage = 0,
			ActFruitCount = 12.0,
			AvgFruitWeight = 23.0,
			DaysWithoutWater = 7
		};

		var options = new Mock<IOptions<WeatherAPIOptions>>();
		options.SetupGet(o => o.Value).Returns(new WeatherAPIOptions {
			Routes = new Routes {WeatherRoute = "https://api.openweathermap.org/data/2.5/weather"},
			APICredentials = new APICredentials {WeatherAPIKey = "ac54d952a883e860998804ec69b90937"}
		});
		var yieldCalcService = new YieldCalcService(options.Object);
		var yieldCalcResponse = yieldCalcService.CalcRequestAsync(yieldCalcRequest).Result;

		yieldCalcResponse.Succeeded.Should().BeFalse();
		yieldCalcResponse.Errors.Should().Contain(YieldCalcResponse.Error.PlantDiedError);
		yieldCalcResponse.Messages.Should().BeEmpty();
	}
}