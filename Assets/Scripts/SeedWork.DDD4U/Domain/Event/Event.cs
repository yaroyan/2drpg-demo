using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.DDD4U.Domain.Event
{
    /// <summary>
    /// Abstract class for domain event.
    /// Implementation by record ensures that it is immutable.
    /// </summary>
    public record Event : IEvent { }
}
