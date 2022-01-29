using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Contracts.V1.Responses;
using Inplanticular.CalculationService.Core.Exceptions;
using Inplanticular.CalculationService.Core.Services;
using Inplanticular.CalculationService.Infrastructure.Calculations;

namespace Inplanticular.CalculationService.Infrastructure.Services;

public class GrowthCalcService : IGrowthCalcService {
	public async Task<GrowthCalcResponse> CalcRequestAsync(GrowthCalcRequest request) {
		GrowthCalcResponse growthCalcResponse;
		try {
			var ripeTimePercentageToday = GrowthCalculation.CalculateRipePercentageToday(
				request.RipePercentageYesterday, request.GrowthPerDay, request.FertilizerPercentageToday,
				request.DaysWithoutWater);
			var ripeTime = GrowthCalculation.CalculateRipeTime(request.TimeFromPlanting, ripeTimePercentageToday);
			growthCalcResponse = new GrowthCalcResponse {
				Succeeded = true,
				RipeTime = ripeTime,
				Messages = new[] {GrowthCalcResponse.Message.GrowthCalculationSuccessfull}
			};
		}
		catch (PlantDeadException e) {
			growthCalcResponse = new GrowthCalcResponse {
				Succeeded = false,
				Errors = new[] {GrowthCalcResponse.Error.PlantDiedError}
			};
		}

		return growthCalcResponse;
	}
}