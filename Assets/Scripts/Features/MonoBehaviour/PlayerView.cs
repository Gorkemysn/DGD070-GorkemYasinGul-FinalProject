using Entitas;
using UnityEngine;

public class PlayerView : EntityView
{
    public override void Initialize(GameEntity entity)
    {
        base.Initialize(entity);
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
}