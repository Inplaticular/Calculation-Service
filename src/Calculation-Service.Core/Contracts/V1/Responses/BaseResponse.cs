using Inplanticular.CalculationService.Core.Contracts.V1.ValueObjects;

namespace Inplanticular.CalculationService.Core.Contracts.V1.Responses;

public class BaseResponse {
	public bool Succeeded { get; set; } = false;
	public IEnumerable<Message> Messages { get; set; } = Enumerable.Empty<Message>();
	public IEnumerable<Message> Errors { get; set; } = Enumerable.Empty<Message>();
}