namespace Inplanticular.CalculationService.Core.Options;

public class WeatherAPIOptions {
	public const string AppSettingsKey = nameof(WeatherAPIOptions);
	public APICredentials APICredentials { get; set; }
	public Routes Routes { get; set; }
}

public class APICredentials {
	public string WeatherAPIKey { get; set; }
}

public class Routes {
	public string WeatherRoute { get; set; }
}