namespace Inplanticular.CalculationService.Core.Contracts.V1.Responses;

public record YieldCalcResponse(bool Success, double Yield, IEnumerable<string> Messages, IEnumerable<string> Errors)
{
    public static class Message
    {
        public const string YieldCalculationSuccessfull = "The yield was estimated successfully";
    }

    public static class Error
    {
        public const string PlantDiedError = "The yield wasnt estimated successfully.";
    }
}