using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Contracts.V1.Responses;
using Inplanticular.CalculationService.Core.Services;

namespace Inplanticular.CalculationService.Infrastructure.Services;

public class YieldCalcService : IYieldCalcService {
	public async Task<YieldCalcResponse> CalcRequestAsync(YieldCalcRequest request) {
		var yieldEst = 0.0;

		if (request.IsWatered)
			yieldEst = request.ActFruitCount * request.AvgFruitWeight * (1+request.FertBuff) * 1.2;
		else
			yieldEst= request.ActFruitCount * request.AvgFruitWeight * (1+request.FertBuff);
		var yieldCalcResponse = new YieldCalcResponse {
			Succeeded = true,
			Yield = yieldEst,
			Messages = new[] {YieldCalcResponse.Message.YieldCalculationSuccessfull}
		};

		return yieldCalcResponse;
	}
}