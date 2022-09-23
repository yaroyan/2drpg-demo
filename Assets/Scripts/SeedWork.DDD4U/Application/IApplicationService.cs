using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD4U.Domain.Event;
using Yaroyan.SeedWork.DDD4U.Application.CQRS.Command;

namespace Yaroyan.SeedWork.DDD4U.Application
{
    public interface IApplicationService
    {
        void Execute(ICommand command);
    }
}
