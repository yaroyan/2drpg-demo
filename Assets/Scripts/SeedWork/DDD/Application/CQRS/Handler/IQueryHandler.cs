using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Event;

namespace Yaroyan.SeedWork.DDD.Application.CQRS.Handler
{
    public interface IQueryHandler { }
    public interface IQueryHandler<in T, out U> : IQueryHandler where T : IQuery<U>
    {
        U Handle();
    }
}
