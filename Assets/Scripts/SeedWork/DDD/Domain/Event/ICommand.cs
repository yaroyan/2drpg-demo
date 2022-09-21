using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SeedWork.DDD.Domain.Event
{
    public interface ICommand { }
    public interface ICommand<T> : ICommand { }
}
