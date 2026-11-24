namespace Roadwise.Application.Common.Exceptions;

public sealed class RoadwiseNotFoundException : Exception
{
    public RoadwiseNotFoundException(string message) : base(message) { }
}
