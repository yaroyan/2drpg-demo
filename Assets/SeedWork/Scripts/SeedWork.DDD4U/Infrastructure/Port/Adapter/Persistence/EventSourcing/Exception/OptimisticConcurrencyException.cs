using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Yaroyan.SeedWork.DDD4U.Domain.Event;
using Yaroyan.SeedWork.DDD4U.Domain.Model;
using System.Runtime.Serialization;

namespace Yaroyan.SeedWork.DDD4U.Infrastructure.Port.Adapter.Persistence.EventSourcing
{
    /// <summary>
    /// Is thrown by event store if there were changes since our last version
    /// </summary>
    [Serializable]
    public class OptimisticConcurrencyException : Exception
    {
        public long ActualVersion { get; private set; }
        public long ExpectedVersion { get; private set; }
        public IEntityId Id { get; private set; }
        public IEnumerable<IEvent> ActualEvents { get; private set; }

        OptimisticConcurrencyException(string message, long actualVersion, long expectedVersion, IEntityId id,
            IEnumerable<IEvent> serverEvents)
            : base(message)
        {
            ActualVersion = actualVersion;
            ExpectedVersion = expectedVersion;
            Id = id;
            ActualEvents = serverEvents;
        }

        public static OptimisticConcurrencyException Create(long actual, long expected, IEntityId id,
            IEnumerable<IEvent> serverEvents)
        {
            var message = string.Format("Expected v{0} but found v{1} in stream '{2}'", expected, actual, id);
            return new OptimisticConcurrencyException(message, actual, expected, id, serverEvents);
        }

        protected OptimisticConcurrencyException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context) { }
    }
}