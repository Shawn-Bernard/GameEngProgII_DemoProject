using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [Header("Debug (read only)")]
    [SerializeField] private string LastActiveState;
    [SerializeField] private string currentActiveState;

    private IState currentState;
    private IState lastState;

    // Instantiate game states
    public MainMenuState mainMenuState = MainMenuState.Instance;
    public GameplayState gameplayState = GameplayState.Instance;

    #region State Machine Updates
    private void Start()
    {
        currentState = gameplayState;
        currentState.EnterState();
    }
    private void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }
    
    private void Update()
    {
        currentState.UpdateState();
    }

    private void LateUpdate()
    {
        currentState.LateUpdateState();
    }

    #endregion

    public void SwitchStates(IState newState)
    {
        lastState = currentState;
        LastActiveState = currentState.ToString();

        currentState?.ExitState();
        

        currentState = newState;
        currentActiveState = currentState.ToString();

        currentState.EnterState();
        
    }
}
