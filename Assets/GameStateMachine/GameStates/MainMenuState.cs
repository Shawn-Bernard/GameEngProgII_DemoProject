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
        gameManager.LevelManager.LoadScene("MainMenu");
        gameManager.UIManager.EnableMainMenu();
    }

    public void FixedUpdateState()
    {

    }

    public void UpdateState()
    {
    }

    public void LateUpdateState()
    {

    }

    public void ExitState()
    {

    }
}