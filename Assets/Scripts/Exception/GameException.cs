using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

public class GameException : Exception
{
    public GameException() { }

    public GameException(string message) : base(message) { }

    public GameException(string message, Exception innerException) : base(message, innerException) { }

    public GameException(Exception innerException) : base(default, innerException) { }

    protected GameException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
