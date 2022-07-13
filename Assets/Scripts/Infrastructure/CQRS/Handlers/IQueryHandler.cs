using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Github.Yaroyan.Rpg.CQRS
{
    public interface IQueryHandler<in T, out U> where T : IQuery<U>
    {
        U Fetch();
    }
}
