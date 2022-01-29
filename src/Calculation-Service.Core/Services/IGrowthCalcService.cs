using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Contracts.V1.Responses;

namespace Inplanticular.CalculationService.Core.Services;

public interface IGrowthCalcService
{
    /// <summary>
    /// Method, that calculates the growth of a plant, thereby checking the water status:<br/>
    ///		0 days without water: 0%<br/>
    ///		1 day without water: -10%<br/>
    ///     2 day without water: -20%<br/>
    ///     3 day without water: -40%<br/>
    ///     4 day without water: -80%<br/>
    ///     5/6 day without water: -100%<br/>
    ///		7 days or more without water: Response with <see cref="Inplanticular.CalculationService.Core.Exceptions.PlantDeadException"/> is returned
    /// </summary>
    /// 
    /// <param name="request">an Object of type <see cref="Inplanticular.CalculationService.Core.Contracts.V1.Requests.GrowthCalcRequest"/></param>
    /// <returns><see cref="Inplanticular.CalculationService.Core.Contracts.V1.Responses.GrowthCalcResponse"/> containing the calculated amount of the next yield and a matching message</returns>
    Task<GrowthCalcResponse> CalcRequestAsync(GrowthCalcRequest request);
}