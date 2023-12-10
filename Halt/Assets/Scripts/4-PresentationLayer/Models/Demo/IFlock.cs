using System.Collections.Generic;
using UnityEngine;

namespace Mole.Halt.PresentationLayer
{
    public interface IFlock
    {
        Vector3 Center { get; }
        Vector3 Heading { get; }
        IEnumerable<InsectView> Boids { get; }
        float GetDistance(InsectView v1, InsectView v2);
    }
}