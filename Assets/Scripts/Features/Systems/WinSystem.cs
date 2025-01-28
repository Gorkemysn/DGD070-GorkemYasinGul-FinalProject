using Entitas;
using UnityEngine;

public class WinSystem : ReactiveSystem<GameEntity>
{
    private readonly Contexts _contexts;
    private readonly GameManager _controller;

    public WinSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _controller = GameObject.FindObjectOfType<GameManager>();
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Triggered);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isTriggered;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities)
    {
        var allPadsTriggered = true;

        foreach (var pad in _contexts.game.GetEntities(GameMatcher.Pad))
        {
            if (!pad.isTriggered)
            {
                allPadsTriggered = false;
                break;
            }
        }

        if (allPadsTriggered)
        {
            var winEntity = _contexts.game.CreateEntity();
            winEntity.isWin = true;
            _controller.ShowWinMessage();
        }
    }
}
