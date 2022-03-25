using Inplanticular.CalculationService.Core.Contracts.V1.Responses;
using Inplanticular.CalculationService.Core.Exceptions;
using Inplanticular.CalculationService.Core.Options;
using Inplanticular.CalculationService.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace Inplanticular.CalculationService.Infrastructure.Calculations;

public static class GrowthCalculation {
	/// <exception cref="InvalidOperationException">Why it's thrown.</exception>
	public static double CalculateRipePercentageToday(int ripePercentageYesterday, double growthPerDay,
		double fertToday, int daysWithoutWater) {
		var waterBuff = daysWithoutWater switch {
			0 => 1.0,
			1 => 0.9,
			2 => 0.8,
			3 => 0.4,
			4 => 0.2,
			5 => 0.0,
			6 => 0.0,
			_ => throw new PlantDeadException()
		};

		return ripePercentageYesterday + (growthPerDay + fertToday) * waterBuff;
	}

	public static int CalculateRipeTime(int timeFromPlanting, double ripePercentageToday) {
		return (int) (100 * timeFromPlanting / ripePercentageToday);
	}

	public static async Task<double> GetRainOfLastThreeHours(double plantCoordinateLatitude,
		double plantCoordinateLongitude, WeatherAPIOptions weatherApiOptions) {
		using var httpClient = new HttpClient();
		var response = await httpClient.SendGetAsync<GetWeatherResponse>(
			string.Format("{0}?lat={1}&lon={2}&appid={3}", weatherApiOptions.Routes.WeatherRoute,
				plantCoordinateLatitude, plantCoordinateLongitude, weatherApiOptions.APICredentials.WeatherAPIKey));

		if (response is null)
			throw new BadHttpRequestException(string.Format("{0}?lat={1}&lon={2}&appid={3}",
				                                  weatherApiOptions.Routes.WeatherRoute,
				                                  plantCoordinateLatitude, plantCoordinateLongitude,
				                                  weatherApiOptions.APICredentials.WeatherAPIKey) +
			                                  "\nRequest did not returned value or response could not be parsed.");

		Console.WriteLine("Wetter an der Position war: " + response.Weather?[0].Description);

		if (response.Rain == null) return 0;
		if (response.Rain._3h is null && response.Rain._1h is not null)
			return response.Rain._1h.Value;
		if (response.Rain._3h is not null)
			return response.Rain._3h.Value;

		return 0;
	}
}