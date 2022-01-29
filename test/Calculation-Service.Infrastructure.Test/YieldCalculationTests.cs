using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Infrastructure.Services;
using Xunit;

namespace YieldCalculationService.Infrastructure.Test;

public class YieldCalculationTests {
	[Fact]
	public void CalcRequestAsync_ShouldReturnResponseWithExpectedYield_WhenWatered() {
		var yieldCalcRequest = new YieldCalcRequest {
			FertilizerPercentage = 0,
			ActFruitCount = 12.0,
			AvgFruitWeight = 23.0,
			DaysWithoutWater = 0
		};
		var yieldCalcService = new YieldCalcService();
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
		var yieldCalcService = new YieldCalcService();
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
		var yieldCalcService = new YieldCalcService();
		var yieldCalcResponse = yieldCalcService.CalcRequestAsync(yieldCalcRequest).Result;
		Assert.NotEmpty(yieldCalcResponse.Errors);
	}
}