using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Github.Yaroyan.Rpg.Entity;
using Yaroyan.SeedWork.DDD.Domain.Event;

namespace Yaroyan.SproutWork.Application.CQRS
{
    public class FetchOneItemQuery : IQuery<Item>
    {
        public int Id { get; private set; }

        public FetchOneItemQuery(int id)
        {
            this.Id = id;
        }
    }
}