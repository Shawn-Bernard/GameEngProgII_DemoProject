using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingState : IState
{
    #region Singleton Instance

    public static GameManager gameManager => GameManager.instance;

    private static readonly LoadingState instance = new LoadingState();

    public static LoadingState Instance = instance;

    #endregion
    public void EnterState()
    {
        Cursor.visible = false;

        Time.timeScale = 0f;

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
