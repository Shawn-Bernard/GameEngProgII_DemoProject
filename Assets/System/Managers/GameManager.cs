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

    public InputManager InputManager => inputManager;

    public GameStateManager GameStateManager => gameStateManager;

    public PlayerController PlayerController => playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        #region Singleton
        // ??= meaning, if (inputManager == null) inputManager = GetComponentInChildren<InputManager>();
        inputManager ??= GetComponentInChildren<InputManager>();
        gameStateManager ??= GetComponentInChildren<GameStateManager>();
        playerController ??= GetComponentInChildren<PlayerController>();

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
