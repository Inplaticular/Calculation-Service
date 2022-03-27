using System.Reflection;
using Inplanticular.CalculationService.Core.Options;
using Inplanticular.CalculationService.Core.Services;
using Inplanticular.CalculationService.Infrastructure.Services;
using Microsoft.OpenApi.Models;

namespace Inplanticular.CalculationService.WebAPI;

public static class Program {
	public static void Main(string[] args) {
		var builder = WebApplication.CreateBuilder(args);
		ConfigureServices(builder);
		var app = builder.Build();
		ConfigurePipeline(app);
		app.Run();
	}

	private static void ConfigureServices(WebApplicationBuilder builder) {
		// Add services to the container.
		builder.Services.Configure<WeatherAPIOptions>(
			builder.Configuration.GetSection(WeatherAPIOptions.AppSettingsKey));
		builder.Services.AddScoped<IGrowthCalcService, GrowthCalcService>();
		builder.Services.AddScoped<IYieldCalcService, YieldCalcService>();
		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c => {
			c.SwaggerDoc("v1", new OpenApiInfo {
				Title = "Caclulation-Service",
				Version = "v1",
				Description =
					"Caclulation-Service of Inplanticular. Used to perform calculations of the yield and growth/ripe time of a plant-",
				Contact = new OpenApiContact {
					Name = "Florian Korch",
					Email = "s0568195@htw-berlin.de",
					Url = new Uri("https://github.com/Inplaticular/Calculation-Service")
				}
			});
			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			c.IncludeXmlComments(xmlPath);
		});
	}

	private static void ConfigurePipeline(WebApplication app) {
		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment()) {
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();
	}
}