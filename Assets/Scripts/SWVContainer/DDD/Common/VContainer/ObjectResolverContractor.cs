using System.Collections;
using System.Collections.Generic;
using VContainer;

namespace Yaroyan.SeedWork.Common.VContainer
{
    public class ObjectResolverContractor<T> : IObjectResolverContractor<T>
    {
        protected readonly IObjectResolver _resolver;

        protected ObjectResolverContractor(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public U Resolve<U>() where U : T => _resolver.Resolve<U>();
    }
}
