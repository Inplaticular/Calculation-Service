using Newtonsoft.Json;

namespace Inplanticular.CalculationService.Core.Contracts.V1.Responses;

public class GetWeatherResponse : BaseResponse {
	public List<Weather>? Weather { get; set; }
	public Rain? Rain { get; set; }
}

public class Weather {
	public int Id { get; set; }
	public string Main { get; set; }
	public string Description { get; set; }
	public string Icon { get; set; }
}

public class Rain {
	[JsonProperty(PropertyName = "1h")] public double? _1h { get; set; }

	[JsonProperty(PropertyName = "3h")] public double? _3h { get; set; }
}