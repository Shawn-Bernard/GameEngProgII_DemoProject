using UnityEngine;

// This help load first 
[DefaultExecutionOrder(-100)]

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }

    [Header("Manager References")]
    public InputManager InputManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        #region Singleton

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        #endregion

        if (InputManager == null)
        {
            InputManager = GetComponentInChildren<InputManager>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
