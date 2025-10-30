using UnityEngine;

// This help load first 
[DefaultExecutionOrder(-100)]

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }

    [Header("Manager References")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private InteractionManager interactionManager;

    public InputManager InputManager => inputManager;

    public GameStateManager GameStateManager => gameStateManager;

    public PlayerController PlayerController => playerController;

    public UIManager UIManager => uiManager;

    public LevelManager LevelManager => levelManager;

    public InteractionManager InteractionManager => interactionManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        #region Singleton
        // ??= meaning, if (inputManager == null) inputManager = GetComponentInChildren<InputManager>();
        inputManager ??= GetComponentInChildren<InputManager>();
        gameStateManager ??= GetComponentInChildren<GameStateManager>();
        playerController ??= GetComponentInChildren<PlayerController>();
        uiManager ??= GetComponentInChildren<UIManager>();
        levelManager ??= GetComponentInChildren<LevelManager>();
        interactionManager ??= GetComponentInChildren<InteractionManager>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        #endregion

        if (inputManager == null)
        {
            inputManager = GetComponentInChildren<InputManager>();
        }
        
    }
}
