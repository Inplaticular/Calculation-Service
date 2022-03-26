using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Contracts.V1.Responses;
using Inplanticular.CalculationService.Core.Options;
using Inplanticular.CalculationService.Core.Services;
using Inplanticular.CalculationService.Infrastructure.Calculations;
using Microsoft.Extensions.Options;

namespace Inplanticular.CalculationService.Infrastructure.Services;

public class YieldCalcService : IYieldCalcService {
	private readonly WeatherAPIOptions _weatherApiOptions;

	public YieldCalcService(IOptions<WeatherAPIOptions> weatherApiOptions) {
		_weatherApiOptions = weatherApiOptions.Value;
	}

	public async Task<YieldCalcResponse> CalcRequestAsync(YieldCalcRequest request) {
		var rain = await GrowthCalculation.GetRainOfLastThreeHours(request.PlantCoordinateLatitude,
			request.PlantCoordinateLongitude,
			_weatherApiOptions);
		if (rain is not 0) request.DaysWithoutWater = 0;

		var yieldEst = 0.0;
		switch (request.DaysWithoutWater) {
			case >= 7: {
				var response = new YieldCalcResponse {
					Succeeded = false,
					Errors = new[] {YieldCalcResponse.Error.PlantDiedError}
				};
				return response;
			}
			case 0:
				if (rain >= 1) {
					var rainBuff = 1 + Math.Pow(10.0, rain.ToString().IndexOf(".")) * rain;
					yieldEst = request.ActFruitCount * request.AvgFruitWeight * (1 + request.FertilizerPercentage) *
					           rainBuff;
				}
				else {
					yieldEst = request.ActFruitCount * request.AvgFruitWeight * (1 + request.FertilizerPercentage) *
					           1.2;
				}

				break;
			default:
				yieldEst = request.ActFruitCount * request.AvgFruitWeight * (1 + request.FertilizerPercentage);
				break;
		}

		var yieldCalcResponse = new YieldCalcResponse {
			Succeeded = true,
			Yield = yieldEst,
			Messages = new[] {YieldCalcResponse.Message.YieldCalculationSuccessful}
		};

		return yieldCalcResponse;
	}
}