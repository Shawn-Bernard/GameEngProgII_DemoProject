using UnityEngine;
using UnityEngine.UIElements;

public class LoadingController : MonoBehaviour
{
    private UIDocument menuUI => GetComponent<UIDocument>();

    GameManager gameManager => GameManager.instance;

    UIManager uiManager => GameManager.instance.UIManager;

    InputManager inputManager => GameManager.instance.InputManager;

    LevelManager levelManager => GameManager.instance.LevelManager;

    private ProgressBar progressBar;

    [SerializeField] private string testString;

    private void OnEnable()
    {
        progressBar = menuUI.rootVisualElement.Q<ProgressBar>("ProgessBar");

    }

    public void UpdateProgressBar(float progress)
    {
        // progress bar is a value between 0 and 1
        progressBar.value = progress;

        // Shows 0-100% (just the integer part)
        progressBar.title = $"{(int)(progress * 100)}%";
    }
}

