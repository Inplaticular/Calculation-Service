using System.ComponentModel.DataAnnotations;

namespace Inplanticular.CalculationService.Core.Contracts.V1.Requests;

public record GrowthCalcRequest {
	[Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
	public int TimeFromPlanting { get; set; }

	[Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
	public int RipePercentageYesterday { get; set; }

	[Range(0, double.MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
	public double GrowthPerDay { get; set; }

	[Required] public double FertilizerPercentageToday { get; set; }
	[Required] [Range(0, int.MaxValue)] public int DaysWithoutWater { get; set; }
}