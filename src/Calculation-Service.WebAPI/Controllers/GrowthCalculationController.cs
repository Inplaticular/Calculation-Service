using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Contracts.V1.Responses;
using Inplanticular.CalculationService.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GrowthCalculationController : ControllerBase
{
    private readonly IGrowthCalcService _growthCalcService;

    private readonly ILogger<GrowthCalculationController> _logger;

    public GrowthCalculationController(ILogger<GrowthCalculationController> logger,
        IGrowthCalcService growthCalcService)
    {
        _logger = logger;
        _growthCalcService = growthCalcService;
    }

    [HttpGet(Name = "calculate")]
    public GrowthCalcResponse GetGrowthCalcRequest(GrowthCalcRequest growthCalcRequest)
    {
        var growthCalcResponse = _growthCalcService.CalcRequestAsync(growthCalcRequest).GetAwaiter().GetResult();
        return growthCalcResponse;
    }
    /*[HttpGet(Name = "testget")]
    public String GetTest() {
        return "Hallo test test";

    }*/
}