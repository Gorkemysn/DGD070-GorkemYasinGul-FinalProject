using Entitas;
using UnityEngine;

public class GameController : Feature {
    public GameController(Contexts contexts) : base("Game Systems") {
        Add(new PlayerInputSystem(contexts));
        Add(new MovementSystem(contexts));
        Add(new BoundarySystem(contexts));
        Add(new TouchSystem(contexts));  
        Add(new PadTriggerSystem(contexts));
        Add(new WinSystem(contexts));
    }
}