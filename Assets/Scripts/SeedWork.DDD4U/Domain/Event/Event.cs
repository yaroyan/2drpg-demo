using System;
using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Model;

namespace Yaroyan.SeedWork.DDD4U.Domain.Event
{
    /// <summary>
    /// Abstract class for domain event.
    /// Implementation by record ensures that it is immutable.
    /// </summary>
    public abstract record Event(string AggregateRootId, long Sequence, DateTime OccuredOn) : IEvent;
}
