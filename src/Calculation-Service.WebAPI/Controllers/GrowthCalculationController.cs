using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Contracts.V1.Responses;
using Inplanticular.CalculationService.Core.Services;
using Inplanticular.CalculationService.WebAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Inplanticular.CalculationService.WebAPI.Controllers;

[ApiController]
[Route("v1/growth")]
public class GrowthCalculationController : ControllerBase {
	private readonly IGrowthCalcService _growthCalcService;
	private readonly ILogger<GrowthCalculationController> _logger;

	public GrowthCalculationController(ILogger<GrowthCalculationController> logger,
		IGrowthCalcService growthCalcService) {
		_logger = logger;
		_growthCalcService = growthCalcService;
	}

	[HttpGet]
	public async Task<IActionResult> GetGrowthCalcRequestAsync(GrowthCalcRequest growthCalcRequest) {
		try {
			return Ok(await _growthCalcService.CalcRequestAsync(growthCalcRequest));
		}
		catch (Exception e) {
			_logger.LogError(e, $"{nameof(GetGrowthCalcRequestAsync)} threw an exception");
			return this.ErrorResponse<GrowthCalcResponse>(e);
		}
	}
}