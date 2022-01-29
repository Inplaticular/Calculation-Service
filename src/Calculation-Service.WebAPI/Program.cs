using Inplanticular.CalculationService.Core.Services;
using Inplanticular.CalculationService.Infrastructure.Services;

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
		builder.Services.AddScoped<IGrowthCalcService, GrowthCalcService>();
		builder.Services.AddScoped<IYieldCalcService, YieldCalcService>();
		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
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