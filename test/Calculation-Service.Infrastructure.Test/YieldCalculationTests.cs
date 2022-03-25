using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Options;
using Inplanticular.CalculationService.Infrastructure.Services;
using Xunit;

namespace YieldCalculationService.Infrastructure.Test;

public class YieldCalculationTests {
	[Fact]
	public void CalcRequestAsync_ShouldReturnResponseWithExpectedYield_WhenWatered() {
		var yieldCalcRequest = new YieldCalcRequest {
			PlantCoordinateLatitude = -34.4251,
			PlantCoordinateLongitude = 50.8931,
			FertilizerPercentage = 0,
			ActFruitCount = 12.0,
			AvgFruitWeight = 23.0,
			DaysWithoutWater = 0
		};
		var yieldCalcService = new YieldCalcService(new WeatherAPIOptions {
			Routes = new Routes {WeatherRoute = "/api.openweathermap.org/data/2.5/weather"},
			APICredentials = new APICredentials {WeatherAPIKey = "ac54d952a883e860998804ec69b90937"}
		});
		var yieldCalcResponse = yieldCalcService.CalcRequestAsync(yieldCalcRequest).Result;
		Assert.Equal(331.2, yieldCalcResponse.Yield);
	}

	[Fact]
	public void CalcRequestAsync_ShouldReturnResponse_WhenNotWateredButNotDead() {
		var yieldCalcRequest = new YieldCalcRequest {
			FertilizerPercentage = 0,
			ActFruitCount = 12.0,
			AvgFruitWeight = 23.0,
			DaysWithoutWater = 3
		};
		var yieldCalcService = new YieldCalcService(new WeatherAPIOptions());
		var yieldCalcResponse = yieldCalcService.CalcRequestAsync(yieldCalcRequest).Result;
		Assert.Equal(276, yieldCalcResponse.Yield);
	}

	[Fact]
	public void CalcRequestAsync_ShouldntReturnResponseWithNegativeYield_WhenNotWatered7Days() {
		var yieldCalcRequest = new YieldCalcRequest {
			FertilizerPercentage = 0,
			ActFruitCount = 12.0,
			AvgFruitWeight = 23.0,
			DaysWithoutWater = 7
		};
		var yieldCalcService = new YieldCalcService(new WeatherAPIOptions());
		var yieldCalcResponse = yieldCalcService.CalcRequestAsync(yieldCalcRequest).Result;
		Assert.NotEmpty(yieldCalcResponse.Errors);
	}
}