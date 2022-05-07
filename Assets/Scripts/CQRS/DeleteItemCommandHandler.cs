using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.Yaroyan.Rpg.CQRS
{
    public class DeleteItemCommandHandler : ICommandHandler<DeleteItemCommand>
    {
        public void Handle(DeleteItemCommand command)
        {
            Debug.Log("command handled: " + command.Id);
        }
    }
}
