using Inplanticular.CalculationService.Core.Contracts.V1.Requests;
using Inplanticular.CalculationService.Core.Contracts.V1.Responses;
using Inplanticular.CalculationService.Core.Exceptions;
using Inplanticular.CalculationService.Core.Services;
using Inplanticular.CalculationService.Infrastructure.Calculations;

namespace Inplanticular.CalculationService.Infrastructure.Services;

public class GrowthCalcService : IGrowthCalcService
{
    public async Task<GrowthCalcResponse> CalcRequestAsync(GrowthCalcRequest request)
    {
        GrowthCalcResponse growthCalcResponse;
        try
        {
            var ripeTimePercentageToday = GrowthCalculation.CalculateRipePercentageToday(
                request.RipePercentageYesterday, request.GrowthPerDay, request.FertToday, request.NoWater,
                request.NoWaterInRow);
            var ripeTime = GrowthCalculation.CalculateRipeTime(request.TimeFromPlanting, ripeTimePercentageToday);
            growthCalcResponse = new GrowthCalcResponse(true, ripeTime,
                new[] {GrowthCalcResponse.Message.GrowthCalculationSuccessfull}, Enumerable.Empty<string>());
        }
        catch (PlantDeadException e)
        {
            growthCalcResponse = new GrowthCalcResponse(false, -1, Enumerable.Empty<string>(),
                new[] {GrowthCalcResponse.Error.PlantDiedError, e.Message});
        }

        return growthCalcResponse;
    }
}