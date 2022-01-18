namespace Inplanticular.CalculationService.Core.Contracts.V1.Requests;

public record YieldCalcRequest(double AvgFruitWeight, double ActFruitCount, bool IsWatered, double FertBuff);