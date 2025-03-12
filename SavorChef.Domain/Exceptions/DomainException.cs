using System.Runtime.Serialization;

namespace SavorChef.Domain.Exceptions;

[Serializable]
public class DomainException(string code, string message, Exception? innerException = null) 
    : Exception(message, innerException)
{
    public string Code { get; } = code;

        /// <summary>
        /// Sets the SerializationInfo with information about the exception
        /// </summary>
        [ObsoleteAttribute]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Code", Code);
        }
    }