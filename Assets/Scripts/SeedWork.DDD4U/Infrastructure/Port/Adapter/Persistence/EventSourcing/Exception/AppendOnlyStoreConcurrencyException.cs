using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing
{
    /// <summary>
    /// Is thrown internally, when storage version does not match the condition 
    /// specified in server request
    /// </summary>
    [Serializable]
    public class AppendOnlyStoreConcurrencyException : Exception
    {
        public long ExpectedStreamVersion { get; private set; }
        public long ActualStreamVersion { get; private set; }
        public string StreamName { get; private set; }

        protected AppendOnlyStoreConcurrencyException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }

        public AppendOnlyStoreConcurrencyException(long expectedVersion, long actualVersion, string name)
            : base(
                string.Format("Expected version {0} in stream '{1}' but got {2}", expectedVersion, name, actualVersion))
        {
            StreamName = name;
            ExpectedStreamVersion = expectedVersion;
            ActualStreamVersion = actualVersion;
        }
    }
}
