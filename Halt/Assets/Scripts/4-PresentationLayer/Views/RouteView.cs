using Mole.Halt.PresentationLayer.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mole.Halt.PresentationLayer.Views
{
    /// <summary>
    /// PROTOTYPE
    /// Representation of a route data model for scene handling.
    /// 
    /// TODO
    /// - Handle internally the waypoint logic, returning Nearest(), First() and Next() upon request
    /// </summary>
    public class RouteView : MonoBehaviour
    {
        [SerializeField] private RouteModel _route = null;

        public List<Transform> Route => _route.Waypoints;
        public EnemyModel Enemy => _route.Enemy;
        /// <summary>
        /// TODO
        /// - draft for migration of the route logic, currently within the Enemy view
        /// </summary>
        /// <returns></returns>
        internal object First()
        {
            throw new NotImplementedException();
        }
    }
}