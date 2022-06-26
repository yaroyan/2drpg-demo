using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Com.Github.Yaroyan.Rpg.CQRS
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
