using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private string spawnPointName;

    private GameStateManager gameStateManager = GameManager.instance.GameStateManager;

    private UIManager uiManager = GameManager.instance.UIManager;

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

        //Loading in my scene name with the string from the trigger
        SceneManager.LoadScene(sceneName);


        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (spawnPointName != null)
        {
            SetPlayerSpawnPoint(spawnPointName);
        }


        //Unsubbing so I don't loop
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    IEnumerable LoadSceneAsync(int sceneID)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        gameStateManager.SwitchStates(LoadingState.Instance);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneID);

        while (asyncLoad.isDone == false)
        {
            float progressValue = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            uiManager.loadingController.UpdateProgessBar(progressValue);
            yield return null;
        }
    }


    private void SetPlayerSpawnPoint(string spawnPointName)
    {
        //Finding my spawn point from the trigger spawn point
        GameObject spawnPoint = GameObject.Find(spawnPointName);

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
}
