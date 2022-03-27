using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Contracts.V1.Responses;
using Inplanticular.CalculationService.Core.Services;
using Inplanticular.CalculationService.WebAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Inplanticular.CalculationService.WebAPI.Controllers;

[ApiController]
[Route("v1/yield")]
public class YieldCalculationController : ControllerBase {
	private readonly ILogger<YieldCalculationController> _logger;
	private readonly IYieldCalcService _yieldCalcService;

	public YieldCalculationController(ILogger<YieldCalculationController> logger,
		IYieldCalcService yieldCalcService) {
		_logger = logger;
		_yieldCalcService = yieldCalcService;
	}

	/// <summary>
	///     Runs a calculation, to determine how much yield (number of fruits combined with average weight of one fruit) the
	///     plant will deliver
	/// </summary>
	/// <param name="yieldCalcRequest"></param>
	[HttpPost]
	[ProducesResponseType(typeof(YieldCalcResponse), 200)]
	public async Task<IActionResult> GetYieldCalcResponseAsync(YieldCalcRequest yieldCalcRequest) {
		try {
			var yieldCalcResponse = await _yieldCalcService.CalcRequestAsync(yieldCalcRequest);
			return Ok(yieldCalcResponse);
		}
		catch (Exception e) {
			_logger.LogError(e, $"{nameof(GetYieldCalcResponseAsync)} threw an exception");
			return this.ErrorResponse<YieldCalcResponse>(e);
		}
	}
}