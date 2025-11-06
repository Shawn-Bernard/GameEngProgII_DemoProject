using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoadState : IState
{
    #region Singleton Instance

    public static GameManager gameManager => GameManager.instance;

    private static readonly BootLoadState instance = new BootLoadState();

    public static BootLoadState Instance = instance;

    #endregion
    public void EnterState()
    {
        Cursor.visible = false;

        Time.timeScale = 0f;

        if (SceneManager.sceneCount == 1 && SceneManager.GetActiveScene().name == "BootLoader")
        {
            // Should add a method that just loads menu
            GameManager.instance.LevelManager.LoadScene("MainMenu");
            gameManager.GameStateManager.SwitchStates(MainMenuState.Instance);
            return;
        }
        else if (SceneManager.sceneCount > 1 && SceneManager.GetActiveScene().name == "MainMenu")
        {
            gameManager.GameStateManager.SwitchStates(MainMenuState.Instance);
        }
        else if (SceneManager.sceneCount > 1 && SceneManager.GetActiveScene().name != "GameOver")
        {
            gameManager.GameStateManager.SwitchStates(GameplayState.Instance);
        }
    }

    public void ExitState()
    {

    }

    public void FixedUpdateState()
    {

    }

    public void LateUpdateState()
    {

    }

    public void UpdateState()
    {

    }
}
