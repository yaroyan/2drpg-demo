using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.Common.VContainer
{
    public interface IObjectResolverContractor<T>
    {
        U Resolve<U>() where U : T;
    }
}
