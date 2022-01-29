using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Contracts.V1.Responses;
using Inplanticular.CalculationService.Core.Services;

namespace Inplanticular.CalculationService.Infrastructure.Services;

public class YieldCalcService : IYieldCalcService {
	public async Task<YieldCalcResponse> CalcRequestAsync(YieldCalcRequest request) {
		var yieldEst = 0.0;

		if (request.DaysWithoutWater >= 7) {
			var yieldCalcResponseErr = new YieldCalcResponse {
				Succeeded = true,
				Yield = -1,
				Errors = new[] {YieldCalcResponse.Error.PlantDiedError}
			};
			return yieldCalcResponseErr;
		}

		if (request.DaysWithoutWater == 0)
			yieldEst = request.ActFruitCount * request.AvgFruitWeight * (1 + request.FertilizerPercentage) * 1.2;
		else
			yieldEst = request.ActFruitCount * request.AvgFruitWeight * (1 + request.FertilizerPercentage);

		var yieldCalcResponse = new YieldCalcResponse {
			Succeeded = true,
			Yield = yieldEst,
			Messages = new[] {YieldCalcResponse.Message.YieldCalculationSuccessfull}
		};

		return yieldCalcResponse;
	}
}