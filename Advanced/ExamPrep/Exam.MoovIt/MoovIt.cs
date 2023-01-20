using System;
using System.Collections.Generic;

namespace Exam.MoovIt
{
    using System.Linq;

    public class MoovIt : IMoovIt
    {
        private Dictionary<string, Route> routes;

        public MoovIt()
        {
            routes = new Dictionary<string, Route>();
        }

        public int Count => routes.Count;

        public void AddRoute(Route route)
        {
            if (this.Contains(route))
            {
                throw new ArgumentException();
            }

            routes.Add(route.Id, route);
        }

        public void ChooseRoute(string routeId)
        {
            if (routes.ContainsKey(routeId))
            {
                routes[routeId].Popularity++;
            }

            throw new ArgumentException();
        }

        public bool Contains(Route route)
        {
            return routes.ContainsKey(route.Id);
        }

        public IEnumerable<Route> GetFavoriteRoutes(string destinationPoint)
        {
            return routes.Values
                .Where(r => r.IsFavorite == true && r.LocationPoints.Contains(destinationPoint) &&
                            r.LocationPoints[0] != destinationPoint)
                .OrderBy(r => r.Distance)
                .ThenByDescending(r => r.Popularity);
        }

        public Route GetRoute(string routeId)
        {
            if (!routes.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }

            return routes[routeId];
        }

        public IEnumerable<Route> GetTop5RoutesByPopularityThenByDistanceThenByCountOfLocationPoints()
        {
            return routes.Values
                .OrderByDescending(r => r.Popularity)
                .ThenBy(r => r.Distance)
                .ThenBy(r => r.LocationPoints.Count);
        }

        public void RemoveRoute(string routeId)
        {
            if (!this.routes.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }

            routes.Remove(routeId);
        }

        public IEnumerable<Route> SearchRoutes(string startPoint, string endPoint)
        {
            var list = routes.Values
                .Where(r => r.LocationPoints.Contains(startPoint) && r.LocationPoints.Contains(endPoint))
                .OrderBy(r => (r.LocationPoints.FindIndex(p => p == endPoint) -
                               r.LocationPoints.FindIndex(p => p == startPoint)))
                .ThenByDescending(r => r.Popularity);
            if (list.Count() == 0 || list == null)
            {
                return list;
            }

            var result = list.Where(r => r.IsFavorite == true).ToList();
            foreach (var route in list)
            {
                if (route.IsFavorite == false)
                {
                    result.Add(route);
                }
            }

            return result;
        }
    }
}
