namespace Roadwise.Application.Common.Exceptions;

public sealed class RoadwiseConflictException : Exception
{
    public RoadwiseConflictException(string message) : base(message) { }
}
