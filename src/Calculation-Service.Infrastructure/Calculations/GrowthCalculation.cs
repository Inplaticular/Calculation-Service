using Inplanticular.CalculationService.Core.Exceptions;

namespace Inplanticular.CalculationService.Infrastructure.Calculations;

public static class GrowthCalculation
{
    /// <exception cref="InvalidOperationException">Why it's thrown.</exception>
    public static double CalculateRipePercentageToday(int ripePercentageYesterday, double growthPerDay,
        double fertToday, bool noWater, int noWaterInRow)
    {
        var waterBuff = 1.0;
        var plantDead = false;
        if (noWater)
            switch (noWaterInRow)
            {
                case 0:
                    waterBuff = 0.9;
                    break;
                case 1:
                    waterBuff = 0.8;
                    break;
                case 2:
                    waterBuff = 0.6;
                    break;
                case 3:
                    waterBuff = 0.2;
                    break;
                case 4:
                    waterBuff = 0.0;
                    break;
                case 5:
                    waterBuff = 0.0;
                    break;
                default:
                    waterBuff = 0.0;
                    plantDead = true;
                    break;
            }

        if (!plantDead)
            return ripePercentageYesterday + (growthPerDay + fertToday) * waterBuff;
        throw new PlantDeadException();
    }

    public static int CalculateRipeTime(int timeFromPlanting, double ripePercentageToday)
    {
        return (int) (100 * timeFromPlanting / ripePercentageToday);
    }
}