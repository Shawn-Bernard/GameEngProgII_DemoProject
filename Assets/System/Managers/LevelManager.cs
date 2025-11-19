using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private string spawnPointName;

    private GameStateManager gameStateManager;

    public UIManager uiManager;

    private GameObject spawnPoint;

    private void Awake()
    {
        gameStateManager = GameManager.instance.GameStateManager;
        uiManager = GameManager.instance.UIManager;
    }

    public void LoadSceneWithSpawnPoint(string sceneName, string triggerSpawnPoint)
    {
        //Throwing in my trigger string into my spawn point name
        spawnPointName = triggerSpawnPoint;

        //Loading in my scene name with the string from the trigger
        SceneManager.LoadScene(sceneName);


        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Loaded scene was called");
        //Loading in my scene name with the string from the trigger
        //SceneManager.LoadScene(sceneName);

        //int SceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
        StartCoroutine(LoadSceneAsync(sceneName));

    }

    IEnumerator LoadSceneAsync(string sceneName)
    {

        gameStateManager.SwitchStates(LoadingState.Instance);

        Debug.Log("LoadSceneAsync started for scene ID: " + sceneName);

        // Wait one frame to ensure UI is properly initialized

        yield return null;

        SceneManager.sceneLoaded += OnSceneLoaded;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Prevent scene activation until we're ready

        asyncLoad.allowSceneActivation = false;

        float artificialProgress = 0f;

        float minUpdateInterval = 0.005f; // Time between updates in seconds

        float maxUpdateInterval = 0.5f; // Time between updates in seconds

        float minProgressIncrement = 0.005f; // Minimum progress increase per update

        float maxProgressIncrement = 0.05f; // Maximum progress increase per update

        float progressCompletedDelayDuration = 1.0f; // Delay after reaching 100% before completing

        while (!asyncLoad.isDone)

        {

            // Progress goes from 0 to 0.9

            float realProgress = asyncLoad.progress;

            // Gradually increase artificial progress

            artificialProgress = Mathf.MoveTowards(

                artificialProgress,

                realProgress,

                Random.Range(minProgressIncrement, maxProgressIncrement)

            );

            if (realProgress >= 0.9f && artificialProgress >= 0.9f)

            {

                // Set progress to 100% before the hold

                artificialProgress = 1.0f;

                uiManager.loadingController.UpdateProgressBar(artificialProgress);

                Debug.Log("Loading completed, holding for display...");

                // Hold at 100% for desired duration

                yield return new WaitForSeconds(progressCompletedDelayDuration);

                Debug.Log("Hold complete, activating scene...");

                // Now allow the scene to activate

                asyncLoad.allowSceneActivation = true;

            }

            else

            {

                // Normalize progress to 0-1 range

                artificialProgress = Mathf.Clamp01(artificialProgress / 0.9f);

            }

            uiManager.loadingController.UpdateProgressBar(artificialProgress);

            // Wait for the specified interval before next update

            yield return new WaitForSeconds(Random.Range(minUpdateInterval, maxUpdateInterval));

        }

    }



    private void SetPlayerSpawnPoint(string spawnPointName)
    {
        if (spawnPointName != null)
        {
            //Finding my spawn point from the trigger spawn point
            spawnPoint = GameObject.Find(spawnPointName);
        }

        Transform targetSpawnPoint = FindAnyObjectByType<PlayerSpawnPoint>().transform;


        //Finding my player object
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //Debug.Log($"Player {player}");

        //Debug.Log($"Spawn {spawnPoint}");
        if (spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;
            player.transform.rotation = spawnPoint.transform.rotation;

        }
        else
        {
            Debug.Log("No spawn point");
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (spawnPointName != null)
        {
            SetPlayerSpawnPoint(spawnPointName);
        }
        Debug.Log("Scene is done loading");
        int SceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (SceneIndex == 1)
        {
            gameStateManager.SwitchStates(MainMenuState.Instance);
        }
        if (SceneIndex > 1)
        {
            gameStateManager.SwitchStates(GameplayState.Instance);
        }

        //Unsubbing so I don't loop
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }
}
