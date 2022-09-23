using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Yaroyan.SeedWork.DDD4U.Application.CQRS.Event;
using Yaroyan.SeedWork.DDD4U.Application.CQRS.Command;
using Yaroyan.SproutWork.Domain.Event.Other;

namespace Yaroyan.SproutWork.Application.CQRS.Command
{
    public class DeleteItemCommandHandler : ICommandHandler<DeleteItemCommand>
    {
        readonly IEventBus _eventBus;
        [Inject]
        public DeleteItemCommandHandler(IEventBus eventBus)
        {
            this._eventBus = eventBus;
        }
        public void Handle(DeleteItemCommand command)
        {
            Debug.Log("command handled: " + command.Id);
            this._eventBus.Publish(new ItemDeletedEvent(command.Id));
        }
    }
}
