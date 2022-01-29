using Inplanticular.CalculationService.Core.Exceptions;

namespace Inplanticular.CalculationService.Infrastructure.Calculations;

public static class GrowthCalculation {
	/// <exception cref="InvalidOperationException">Why it's thrown.</exception>
	public static double CalculateRipePercentageToday(int ripePercentageYesterday, double growthPerDay,
		double fertToday, int daysWithoutWater) {
		var waterBuff = 1.0;
		switch (daysWithoutWater) {
			case 0:
				waterBuff = 1.0;
				break;
			case 1:
				waterBuff = 0.9;
				break;
			case 2:
				waterBuff = 0.8;
				break;
			case 3:
				waterBuff = 0.4;
				break;
			case 4:
				waterBuff = 0.2;
				break;
			case 5:
				waterBuff = 0.0;
				break;
			case 6:
				waterBuff = 0.0;
				break;
			default:
				throw new PlantDeadException();
		}

		return ripePercentageYesterday + (growthPerDay + fertToday) * waterBuff;
	}

	public static int CalculateRipeTime(int timeFromPlanting, double ripePercentageToday) {
		return (int) (100 * timeFromPlanting / ripePercentageToday);
	}
}