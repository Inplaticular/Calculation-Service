using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Contracts.V1.Responses;
using Inplanticular.CalculationService.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class YieldCalculationController : ControllerBase {
	private readonly ILogger<YieldCalculationController> _logger;
	private readonly IYieldCalcService _yieldCalcService;

	public YieldCalculationController(ILogger<YieldCalculationController> logger,
		IYieldCalcService yieldCalcService) {
		_logger = logger;
		_yieldCalcService = yieldCalcService;
	}

	[HttpGet(Name = "calculateYield")]
	public YieldCalcResponse GetYieldCalcResponse(YieldCalcRequest yieldCalcRequest) {
		var yieldCalcResponse = _yieldCalcService.CalcRequestAsync(yieldCalcRequest).GetAwaiter().GetResult();
		return yieldCalcResponse;
	}
}