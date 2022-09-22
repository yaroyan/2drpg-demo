using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Event;
using Yaroyan.SeedWork.DDD.Application.CQRS.Command;

namespace Yaroyan.SeedWork.DDD.Application
{
    public interface IApplicationService
    {
        void Execute(ICommand command);
    }
}
