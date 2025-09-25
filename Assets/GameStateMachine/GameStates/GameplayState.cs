using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayState : IState
{
    #region Singleton Instance

    private static readonly GameplayState instance = new GameplayState();

    public static GameplayState Instance = instance;

    #endregion
    public void EnterState() 
    {
        Debug.Log("Entered gameplay state");
    }

    public void FixedUpdateState()
    {

    }

    public void UpdateState()
    {
        //GameManager.instance.PlayerController.HandleMovement();
        if (Keyboard.current[Key.Digit1].wasPressedThisFrame)
        {
            //GameManager.instance.GameStateManager.SwitchStates();
        }
        if (Keyboard.current[Key.Digit2].wasPressedThisFrame)
        {
            //GameManager.instance.GameStateManager.SwitchStates();
        }
    }

    public void LateUpdateState()
    {
        //GameManager.instance.PlayerController.HandleLook();
    }

    public void ExitState()
    {

    }
}
