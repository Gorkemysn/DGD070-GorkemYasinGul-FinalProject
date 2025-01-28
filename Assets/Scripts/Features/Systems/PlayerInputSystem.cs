using Entitas;
using UnityEngine;

public class PlayerInputSystem : IExecuteSystem
{
    private readonly GameContext _gameContext;
    private const float _speed = 5f;

    public PlayerInputSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
    }

    public void Execute()
    {
        var playerEntities = _gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Position, GameMatcher.Velocity));

        foreach (var entity in playerEntities)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");

            Vector3 moveVector = new Vector3(moveX, 0f, moveZ);
            if (moveVector.sqrMagnitude > 0)
            {
                moveVector = moveVector.normalized * _speed;
            }

            entity.ReplaceVelocity(moveVector);
        }
    }
}
