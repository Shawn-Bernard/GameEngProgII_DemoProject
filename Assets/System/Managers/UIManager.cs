using UnityEngine;
using UnityEngine.UIElements;
public class UIManager : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public GameObject gameplayMenuUI;
    public GameObject mainMenuUI;
    public GameObject gameOverMenuUI;

    private void Awake()
    {
        DisableAllMenus();
    }

    public void DisableAllMenus()
    {
        mainMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        gameplayMenuUI.SetActive(false);
        gameOverMenuUI.SetActive(false);
    }

    public void EnableMainMenu()
    {
        DisableAllMenus();
        mainMenuUI.SetActive(true);
    }

    public void EnablePauseMenu()
    {
        DisableAllMenus();
        pauseMenuUI.SetActive(true);
    }

    public void EnableGameplayMenu()
    {
        DisableAllMenus();
        gameplayMenuUI.SetActive(true);
    }

    public void EnableGameOverMenu()
    {
        DisableAllMenus();
        gameOverMenuUI.SetActive(true);
    }
}
