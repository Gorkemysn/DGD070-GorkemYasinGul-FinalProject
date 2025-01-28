using Entitas;
using UnityEngine;
[Game]
public class BoundaryComponent : IComponent {
    public Vector3 minBounds;  
    public Vector3 maxBounds;  
}