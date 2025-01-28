using Entitas;
using UnityEngine;

public class BoundarySystem : IExecuteSystem {
    readonly GameContext _context;
    private readonly float _boundaryX = 8f;
    private readonly float _boundaryZ = 4.5f;
    
    public BoundarySystem(Contexts contexts) {
        _context = contexts.game;
    }
    
    public void Execute() {
        var entities = _context.GetEntities(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.Player));
        
        foreach (var e in entities) {
            var position = e.position.position;
            
            position.x = Mathf.Clamp(position.x, -_boundaryX, _boundaryX);
            position.z = Mathf.Clamp(position.z, -_boundaryZ, _boundaryZ);
            
            e.ReplacePosition(position);
        }
    }
}