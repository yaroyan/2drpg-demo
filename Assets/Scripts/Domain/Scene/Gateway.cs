using System.Collections;
using System.Collections.Generic;
using System;

namespace Yaroyan.Game.RPG.Domain.Model.Scene
{
    public class Gateway : Location
    {
        List<Route> _routes;
        public IReadOnlyList<Route> Routes { get => _routes; }
        public bool IsOneWay { get; private set; }
        static readonly int s_MinRouteCount = 1;

        public Gateway(List<Route> routes, bool isOneWay, LocationId id, SceneId sceneId, string name, string description) : base(id, sceneId, name, description)
        {
            this._routes = routes;
            this.IsOneWay = isOneWay;
        }

        public void DiscoverRoute(Route route)
        {
            this._routes.Add(route);
        }

        public void UncontinueRoute(Route route)
        {
            if (_routes.Count == s_MinRouteCount) throw new InvalidOperationException($"{nameof(Routes)} requires {s_MinRouteCount} or more.");
            this._routes.Remove(route);
        }
    }
}
