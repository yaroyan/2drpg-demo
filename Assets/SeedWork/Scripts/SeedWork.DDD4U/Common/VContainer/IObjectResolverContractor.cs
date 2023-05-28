using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.DDD4U.Common.VContainer
{
    public interface IObjectResolverContractor<T>
    {
        U Resolve<U>() where U : T;
    }
}
