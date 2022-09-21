using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Github.Yaroyan.Rpg.Entity;
using Yaroyan.SeedWork.DDD.Application.CQRS;
using Yaroyan.SeedWork.DDD.Application.CQRS.Handler;

namespace Yaroyan.SproutWork.Application.CQRS
{
    public class FetchOneItemHandler : IQueryHandler<FetchOneItemQuery, Item>
    {
        readonly FetchOneItemQuery _query;
        public FetchOneItemHandler(FetchOneItemQuery query)
        {
            this._query = query;
        }

        public Item Handle()
        {
            throw new System.NotImplementedException();
        }
    }
}
