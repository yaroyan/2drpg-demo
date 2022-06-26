using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.Yaroyan.Rpg.CQRS
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
