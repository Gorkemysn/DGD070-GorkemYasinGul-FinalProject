using UnityEngine;
using Entitas;
using TMPro;

public class GameManager : MonoBehaviour
{
    private Systems _systems;
    private EntityCreate _entityCreator;
    public TextMeshProUGUI winText;
    public Camera mainCamera;

    private static readonly Color[] targetColors = { Color.red, Color.blue, Color.yellow, Color.magenta };
    private int currentTargetIndex = 0;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        _entityCreator = GetComponent<EntityCreate>();
        _systems = new Feature("Systems")
            .Add(new PlayerInputSystem(Contexts.sharedInstance))
            .Add(new MovementSystem(Contexts.sharedInstance))
            .Add(new BoundarySystem(Contexts.sharedInstance))
            .Add(new TouchSystem(Contexts.sharedInstance))
            .Add(new PadTriggerSystem(Contexts.sharedInstance))
            .Add(new WinSystem(Contexts.sharedInstance));

        _entityCreator.CreatePlayer(Vector3.zero);
        _entityCreator.CreatePad(new Vector3(-6f, 0f, 3f), 0);
        _entityCreator.CreatePad(new Vector3(6f, 0f, 3f), 1);
        _entityCreator.CreatePad(new Vector3(-6f, 0f, -3f), 2);
        _entityCreator.CreatePad(new Vector3(6f, 0f, -3f), 3);

        UpdateBackgroundColor(); // Ýlk hedef rengi belirle
    }

    void Update()
    {
        _systems.Execute();
    }

    void OnDestroy()
    {
        _systems.TearDown();
    }

    public void ShowWinMessage()
    {
        winText.gameObject.SetActive(true);
        winText.text = "A WINRAR IS YOU";
        Time.timeScale = 0;
    }

    private void UpdateBackgroundColor()
    {
        mainCamera.backgroundColor = targetColors[currentTargetIndex];
    }

    public bool CanTriggerPad(Color padColor)
    {
        // Sadece sýradaki pad basýlabilir
        return padColor == targetColors[currentTargetIndex];
    }

    public void AdvanceToNextColor()
    {
        if (currentTargetIndex < targetColors.Length - 1)
        {
            currentTargetIndex++;
            UpdateBackgroundColor();
        }
        else
        {
            ShowWinMessage(); // Tüm padlere sýrasýyla basýldýysa kazandýk
        }
    }
}
