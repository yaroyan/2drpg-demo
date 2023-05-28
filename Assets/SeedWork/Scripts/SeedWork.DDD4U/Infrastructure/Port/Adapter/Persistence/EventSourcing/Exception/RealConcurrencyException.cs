using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing
{
    /// <summary>
    /// Is supposed to be thrown by the client code, when it fails to resolve concurrency problem
    /// </summary>
    [Serializable]
    public class RealConcurrencyException : Exception
    {
        public RealConcurrencyException() { }
        public RealConcurrencyException(string message) : base(message) { }
        public RealConcurrencyException(string message, Exception inner) : base(message, inner) { }

        protected RealConcurrencyException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
