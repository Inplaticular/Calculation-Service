using System.ComponentModel.DataAnnotations;

namespace Inplanticular.CalculationService.Core.Contracts.V1.Requests;

public record YieldCalcRequest {
	[Range(1, double.MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
	public double AvgFruitWeight { get; set; }

	[Range(0, double.MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
	public double ActFruitCount { get; set; }

	[Required] public bool IsWatered { get; set; }
	[Required] public double FertBuff { get; set; }
}