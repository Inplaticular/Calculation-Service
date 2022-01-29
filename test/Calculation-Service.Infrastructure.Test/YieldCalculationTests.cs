using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Infrastructure.Services;
using Xunit;

namespace YieldCalculationService.Infrastructure.Test;

public class YieldCalculationTests {
	[Fact]
	public void CalcRequestAsync_ShouldReturnResponse_WhenWatered() {
		var yieldCalcRequest = new YieldCalcRequest {
			FertBuff = 1.0,
			ActFruitCount = 12.0,
			AvgFruitWeight = 23.0,
			IsWatered = true
		};
		var yieldCalcService = new YieldCalcService();
		var yieldCalcResponse = yieldCalcService.CalcRequestAsync(yieldCalcRequest).Result;
		Assert.Equal(331.2, yieldCalcResponse.Yield);
	}

	[Fact]
	public void CalcRequestAsync_ShouldReturnResponse_WhenWatered2() {
		var yieldCalcRequest = new YieldCalcRequest {
			FertBuff = 1,
			ActFruitCount = 12.0,
			AvgFruitWeight = 23.0,
			IsWatered = true
		};
		var yieldCalcService = new YieldCalcService();
		var yieldCalcResponse = yieldCalcService.CalcRequestAsync(yieldCalcRequest).Result;
		Assert.Equal(331.2, yieldCalcResponse.Yield);
	}
}