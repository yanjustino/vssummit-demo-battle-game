using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Domain.Entities;

[Serializable]
[ExcludeFromCodeCoverage]
public class EntityArgumentException: Exception
{
    public EntityArgumentException(string message): base(message)
    {
    }

    public EntityArgumentException(string message, Exception innerException): base(message, innerException)
    {
    }
    
    public EntityArgumentException(SerializationInfo info, StreamingContext context): base(info, context)
    {
    }
}