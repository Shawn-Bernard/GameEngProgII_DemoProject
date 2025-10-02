using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuState : IState
{
    #region Singleton Instance

    public static GameManager gameManager => GameManager.instance;

    private static readonly MainMenuState instance = new MainMenuState();

    public static MainMenuState Instance = instance;

    #endregion
    public void EnterState()
    {
        //Time.timeScale = 0;
        gameManager.UIManager.EnableMainMenu();
    }

    public void FixedUpdateState()
    {

    }

    public void UpdateState()
    {
        if (Keyboard.current[Key.Digit1].wasPressedThisFrame)
        {
            Debug.Log("Switched to main menu state");
            GameManager.instance.GameStateManager.SwitchStates(new MainMenuState());
        }
        if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
        {
            Debug.Log("Switched to gameplay state");
            GameManager.instance.GameStateManager.SwitchStates(new GameplayState());
        }
        if (Keyboard.current[Key.Digit3].wasPressedThisFrame)
        {
            Debug.Log("Switched to pause state");
            GameManager.instance.GameStateManager.SwitchStates(new PauseState());
        }
    }

    public void LateUpdateState()
    {

    }

    public void ExitState()
    {

    }
}