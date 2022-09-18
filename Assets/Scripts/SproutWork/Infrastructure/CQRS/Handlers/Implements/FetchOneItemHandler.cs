using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Github.Yaroyan.Rpg.Entity;

namespace Com.Github.Yaroyan.Rpg.CQRS
{
    public class FetchOneItemHandler : IQueryHandler<FetchOneItemQuery, Item>
    {
        readonly FetchOneItemQuery _query;
        public FetchOneItemHandler(FetchOneItemQuery query)
        {
            this._query = query;
        }

        public Item Fetch()
        {
            throw new System.NotImplementedException();
        }
    }
}
