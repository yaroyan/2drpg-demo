using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Domain.Event;

namespace Yaroyan.SproutWork.Application.CQRS
{
    public class DeleteItemCommand : ICommand
    {
        public int Id { get; private set; }
        public DeleteItemCommand(int id)
        {
            Id = id;
        }
    }
}
