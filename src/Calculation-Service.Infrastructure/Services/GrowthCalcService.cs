using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Contracts.V1.Responses;
using Inplanticular.CalculationService.Core.Exceptions;
using Inplanticular.CalculationService.Core.Options;
using Inplanticular.CalculationService.Core.Services;
using Inplanticular.CalculationService.Infrastructure.Calculations;
using Microsoft.Extensions.Options;

namespace Inplanticular.CalculationService.Infrastructure.Services;

public class GrowthCalcService : IGrowthCalcService {
	private readonly WeatherAPIOptions _weatherApiOptions;

	public GrowthCalcService(IOptions<WeatherAPIOptions> weatherApiOptions) {
		_weatherApiOptions = weatherApiOptions.Value;
	}

	public async Task<GrowthCalcResponse> CalcRequestAsync(GrowthCalcRequest request) {
		GrowthCalcResponse growthCalcResponse;

		try {
			var rain = await GrowthCalculation.GetRainOfLastThreeHours(request.PlantCoordinateLatitude,
				request.PlantCoordinateLongitude,
				_weatherApiOptions);
			if (rain is not 0) request.DaysWithoutWater = 0;
			var ripeTimePercentageToday = GrowthCalculation.CalculateRipePercentageToday(
				request.RipePercentageYesterday, request.GrowthPerDay, request.FertilizerPercentageToday,
				request.DaysWithoutWater);
			var ripeTime = GrowthCalculation.CalculateRipeTime(request.TimeFromPlanting, ripeTimePercentageToday);
			growthCalcResponse = new GrowthCalcResponse {
				Succeeded = true,
				GrowthPercentage = ripeTimePercentageToday,
				RipeTime = ripeTime,
				Messages = new[] {GrowthCalcResponse.Message.GrowthCalculationSuccessful}
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