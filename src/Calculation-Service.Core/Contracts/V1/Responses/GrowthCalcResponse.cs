﻿using System.ComponentModel.DataAnnotations;

namespace Inplanticular.CalculationService.Core.Contracts.V1.Responses;

public class GrowthCalcResponse : BaseResponse {
	[Range(0, double.MaxValue)] public double GrowthPercentage { get; set; }
	[Range(0, int.MaxValue)] public int RipeTime { get; set; }

	public static class Message {
		public static readonly ValueObjects.Message
			GrowthCalculationSuccessful = new() {
				Code = nameof(GrowthCalculationSuccessful),
				Description = "The ripeTime was calculated successfully."
			};
	}

	public static class Error {
		public static readonly ValueObjects.Message
			PlantDiedError = new() {
				Code = nameof(PlantDiedError),
				Description = "The ripeTime wasn't calculated successfully. The plant died due to a lack of water."
			};
	}
}