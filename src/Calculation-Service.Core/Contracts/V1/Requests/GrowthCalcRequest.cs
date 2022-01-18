namespace Inplanticular.CalculationService.Core.Contracts.V1.Requests;

public record GrowthCalcRequest(int TimeFromPlanting, int RipePercentageYesterday, double GrowthPerDay,
    double FertToday, bool NoWater, int NoWaterInRow);