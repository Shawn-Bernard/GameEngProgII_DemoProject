using System;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour,Inputs.IPlayerActions
{

    private Inputs inputs;

    void Awake()
    {
        try
        {
            inputs = new Inputs();

            inputs.Player.SetCallbacks(this);
            inputs.Player.Enable();
        }
        catch (Exception exception) 
        {
            Debug.LogError("Input error" + exception);
        }
        
    }

    #region Input Events

    public event Action<Vector2> MoveInputEvent;

    public event Action<Vector2> LookInputEvent;

    public event Action JumpStartedInputEvent;
    public event Action JumpFinishedInputEvent;
    public event Action JumpCancelledInputEvent;

    #endregion

    #region Input Callbacks

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInputEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInputEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpStartedInputEvent?.Invoke();

        }
        if (context.performed)
        {
            JumpFinishedInputEvent?.Invoke();
        }
        if (context.canceled)
        {
            JumpCancelledInputEvent?.Invoke();
        }
    }

    #endregion


    void OnEnable()
    {
        if (inputs != null)
        {
            inputs.Player.Enable();
        }
    }

    void OnDestroy()
    {
        if (inputs != null)
        {
            inputs.Player.Disable();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
