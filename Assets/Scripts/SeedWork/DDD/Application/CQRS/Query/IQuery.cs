using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.DDD.Application.CQRS.Query
{
    public interface IQuery { }
    public interface IQuery<T> : IQuery { }
}
