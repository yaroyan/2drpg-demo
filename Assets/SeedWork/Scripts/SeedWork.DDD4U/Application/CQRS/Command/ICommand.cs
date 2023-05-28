using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.DDD4U.Application.CQRS.Command
{
    public interface ICommand { }
    public interface ICommand<T> : ICommand { }
}
