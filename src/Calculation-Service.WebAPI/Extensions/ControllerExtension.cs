using Inplanticular.CalculationService.Core.Contracts.V1.Responses;
using Inplanticular.CalculationService.Core.Contracts.V1.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Inplanticular.CalculationService.WebAPI.Extensions;

public static class ControllerExtension {
	public static IActionResult ErrorResponse<TResponse>(this ControllerBase controller, Exception exception)
		where TResponse : BaseResponse {
		var response = Activator.CreateInstance<TResponse>();
		response.Errors = new[] {
			new Message {
				Code = exception.GetType().Name,
				Description = exception.Message
			}
		};
		return controller.StatusCode(500, response);
	}
}