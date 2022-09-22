using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.DDD.Application.CQRS.Command
{
    public interface ICommand { }
    public interface ICommand<T> : ICommand { }
}
