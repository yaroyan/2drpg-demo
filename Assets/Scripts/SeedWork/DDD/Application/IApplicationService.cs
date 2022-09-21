using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Event;

namespace Yaroyan.SeedWork.DDD.Application
{
    public interface IApplicationService
    {
        void Execute(ICommand command);
    }
}
