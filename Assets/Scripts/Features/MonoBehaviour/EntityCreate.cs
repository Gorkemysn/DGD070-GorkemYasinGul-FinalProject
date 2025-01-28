using UnityEngine;
using Entitas;
using System.Collections.Generic;

public class EntityCreate : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject padPrefab;

    private GameContext _context;

    void Start()
    {
        _context = Contexts.sharedInstance.game;
    }

    public void CreatePlayer(Vector3 position)
    {
        var entity = _context.CreateEntity();
        entity.isPlayer = true;
        entity.AddPosition(position);
        entity.AddVelocity(Vector3.zero);

        var playerGo = Instantiate(playerPrefab, position, Quaternion.identity);
        var view = playerGo.GetComponent<EntityView>();
        view.Initialize(entity);
    }

    public void CreatePad(Vector3 position, int id)
    {
        var entity = _context.CreateEntity();
        entity.AddPad(id);
        entity.AddPosition(position);

        var padGo = Instantiate(padPrefab, position, Quaternion.identity);
        var view = padGo.GetComponent<PadView>();
        view.Initialize(entity);
        ((PadView)view).InitializePad(id); // Rengi belirle

        Color[] padColors = { Color.red, Color.blue, Color.yellow, Color.magenta };
        view.SetColor(padColors[id]);
    }
}
