using UnityEngine;
using Entitas;
using System.Linq;

public class TouchSystem : IExecuteSystem
{
    private readonly GameContext _gameWorld;
    private const float TriggerRadius = 1f;

    public TouchSystem(Contexts contexts)
    {
        _gameWorld = contexts.game;
    }

    public void Execute()
    {
        var playerEntity = _gameWorld.GetEntities(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Position))
            .FirstOrDefault();

        if (playerEntity == null) return;

        var padEntities = _gameWorld.GetEntities(GameMatcher.AllOf(GameMatcher.Pad, GameMatcher.Position));
        var playerPosition = playerEntity.position.position;

        foreach (var pad in padEntities)
        {
            if (!pad.isTriggered && Vector3.Distance(playerPosition, pad.position.position) < TriggerRadius)
            {
                pad.isTriggered = true;
                Debug.Log("Pad activated!");
            }
        }
    }
}
