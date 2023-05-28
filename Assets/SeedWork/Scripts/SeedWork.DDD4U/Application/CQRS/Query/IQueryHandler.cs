using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Event;

namespace Yaroyan.SeedWork.DDD4U.Application.CQRS.Query
{
    public interface IQueryHandler { }
    public interface IQueryHandler<in T, out U> : IQueryHandler where T : IQuery<U>
    {
        U Handle();
    }
}
