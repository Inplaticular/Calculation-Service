namespace Inplanticular.CalculationService.Core.Contracts.V1.Responses;

public record GrowthCalcResponse(bool Success, int RipeTime, IEnumerable<string> Messages, IEnumerable<string> Errors)
{
    public static class Message
    {
        public const string GrowthCalculationSuccessfull = "The ripeTime was calculated successfully";
    }

    public static class Error
    {
        public const string PlantDiedError =
            "The ripeTime wasnt calculated successfully. The plant died due to a lack of water.";
    }
}