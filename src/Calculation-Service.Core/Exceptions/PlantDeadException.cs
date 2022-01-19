using System.Runtime.Serialization;

namespace Inplanticular.CalculationService.Core.Exceptions;

[Serializable]
public class PlantDeadException : Exception
{
    public PlantDeadException()
    {
    }

    public PlantDeadException(string message) : base(message)
    {
    }

    public PlantDeadException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected PlantDeadException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}