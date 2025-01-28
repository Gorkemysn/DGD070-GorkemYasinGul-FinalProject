using UnityEngine;
using Entitas;

public class EntityView : MonoBehaviour
{
    protected GameEntity _entity;

    public virtual void Initialize(GameEntity entity)
    {
        _entity = entity;
        transform.position = _entity.position.position;
    }

    public void Update()
    {
        if (_entity != null && _entity.hasPosition)
        {
            transform.position = _entity.position.position;
        }
    }
}