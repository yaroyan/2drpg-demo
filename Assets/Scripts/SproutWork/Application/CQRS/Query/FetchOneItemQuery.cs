using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaroyan.SproutWork.Domain.Model.Other;
using Yaroyan.SeedWork.DDD.Domain.Event;
using Yaroyan.SeedWork.DDD.Application.CQRS.Query;

namespace Yaroyan.SproutWork.Application.CQRS.Query
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