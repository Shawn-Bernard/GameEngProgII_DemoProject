using UnityEngine;
using UnityEngine.UIElements;

public class GameplayMenu : MonoBehaviour
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
        progressBar = menuUI.rootVisualElement.Q<ProgressBar>("LoadingProgessBar");

    }

    public void UpdateProgessBar(float progess)
    {
        progressBar.value = progess;

        progressBar.title = $"{(int)progess * 100}%";
    }
}
