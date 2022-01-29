using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Contracts.V1.Responses;

namespace Inplanticular.CalculationService.Core.Services;

public interface IYieldCalcService
{
    /// <summary>
    /// Method, that calculates the yield of a plant, thereby checking the water status:<br/>
    ///		0 days without water: buffed in growth by 0.2%<br/>
    ///		1-6 days without water: no buff<br/>
    ///		7 days or more without water: Response with <see cref="Inplanticular.CalculationService.Core.Exceptions.PlantDeadException"/> is returned 
    /// </summary>
    /// 
    /// <param name="request">an Object of type <see cref="Inplanticular.CalculationService.Core.Contracts.V1.Requests.YieldCalcRequest"/></param>
    /// <returns><see cref="Inplanticular.CalculationService.Core.Contracts.V1.Responses.YieldCalcResponse"/> containing the calculated amount of the next yield and a matching message</returns>
    Task<YieldCalcResponse> CalcRequestAsync(YieldCalcRequest request);
}