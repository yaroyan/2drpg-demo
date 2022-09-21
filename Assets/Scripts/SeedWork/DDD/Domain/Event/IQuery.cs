using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.DDD.Domain.Event
{
    public interface IQuery { }
    public interface IQuery<T> : IQuery { }
}
