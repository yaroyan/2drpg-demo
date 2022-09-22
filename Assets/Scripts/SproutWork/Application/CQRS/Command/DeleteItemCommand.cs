using System.Collections;
using System.Collections.Generic;
using Yaroyan.SeedWork.DDD.Application.CQRS.Command;

namespace Yaroyan.SproutWork.Application.CQRS.Command
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
