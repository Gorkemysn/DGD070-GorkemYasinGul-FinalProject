using Entitas;
using UnityEngine;
using System.Collections.Generic;

public sealed class PadTriggerSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _gameState;

    public PadTriggerSystem(Contexts contexts) : base(contexts.game)
    {
        _gameState = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Triggered);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPad && entity.isTriggered;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var totalPads = _gameState.GetEntities(GameMatcher.Pad);
        var activatedPads = _gameState.GetEntities(GameMatcher.AllOf(GameMatcher.Pad, GameMatcher.Triggered));

        if (totalPads.Length == activatedPads.Length)
        {
            var winEntity = _gameState.CreateEntity();
            winEntity.isWin = true;

            var players = _gameState.GetEntities(GameMatcher.Player);
            foreach (var player in players)
            {
                player.Destroy();
            }
        }
    }
}