using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Contracts.V1.Responses;

namespace Inplanticular.CalculationService.Core.Services;

public interface IGrowthCalcService
{
    Task<GrowthCalcResponse> CalcRequestAsync(GrowthCalcRequest request);
}