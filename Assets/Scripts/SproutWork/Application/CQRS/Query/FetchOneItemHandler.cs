using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaroyan.SproutWork.Domain.Model.Other;
using Yaroyan.SeedWork.DDD.Application.CQRS.Query;

namespace Yaroyan.SproutWork.Application.CQRS.Query
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
