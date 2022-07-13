using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Github.Yaroyan.Rpg.Entity;

namespace Com.Github.Yaroyan.Rpg.CQRS
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