using Entitas;
using UnityEngine;

public class MovementSystem : IExecuteSystem {
    readonly GameContext _context;
    
    public MovementSystem(Contexts contexts) {
        _context = contexts.game;
    }

    public void Execute()
    {
        var movingEntities = _context.GetEntities(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.Velocity));

        foreach (var entity in movingEntities)
        {
            Vector3 pos = entity.position.position;
            Vector3 vel = entity.velocity.velocity;

            if (vel.sqrMagnitude > 0)
            {
                Vector3 updatedPos = pos + vel * Time.deltaTime;
  

                entity.ReplacePosition(updatedPos);
            }
        }
    }
}