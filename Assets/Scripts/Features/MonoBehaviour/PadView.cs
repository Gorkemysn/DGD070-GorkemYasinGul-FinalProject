using UnityEngine;
using Entitas;

public class PadView : EntityView
{
    private MeshRenderer _renderer;
    public Color originalColor; // Pad'in ilk rengini saklamak için

    private static readonly Color[] padColors = { Color.red, Color.blue, Color.yellow, Color.magenta };

    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void InitializePad(int id)
    {
        originalColor = padColors[id % padColors.Length]; // ID'ye göre renk seç
        _renderer.material.color = originalColor;
    }

    public new void Update()
    {
        base.Update();

        if (_entity != null && _entity.isTriggered)
        {
            if (GameManager.Instance.CanTriggerPad(originalColor))
            {
                _renderer.material.color = Color.green;
                GameManager.Instance.AdvanceToNextColor();
                _entity.isTriggered = false;
            }
            else
            {
                _entity.isTriggered = false;
            }
        }
    }


    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }
}
