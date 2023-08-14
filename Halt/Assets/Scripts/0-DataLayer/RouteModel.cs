using System.Collections.Generic;
using UnityEngine;


namespace Mole.Halt.PresentationLayer.Models
{
    /// <summary>
    /// Route data model. Converts lighweight Vector3 points into Transforms at runtime.
    /// 
    /// TODO
    /// - Handle in-scene transforms parent
    /// </summary>
    [CreateAssetMenu(fileName = "asset-route-generic", menuName = "Mole/Joking Sam/Route")]
    public class RouteModel : ScriptableObject
    {
        [SerializeField] private EnemyModel _enemyType = null;
        [SerializeField] private List<Vector3> Positions = null;

        public EnemyModel Enemy => _enemyType;
        /// <summary>
        /// Returns a ready-to-use list of waypoints
        /// </summary>
        public List<Transform> Waypoints
        {
            get
            {
                GameObject gameobject;
                Transform transform;
                List<Transform> points = new List<Transform>(Positions.Count);
                foreach (Vector3 position in Positions)
                {
                    gameobject = new GameObject();
                    transform = gameobject.transform;
                    transform.position = position;

                    points.Add(transform);
                }
                return points;
            }
        }
    }
}