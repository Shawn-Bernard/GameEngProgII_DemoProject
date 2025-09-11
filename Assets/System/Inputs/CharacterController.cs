using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    
    public InputManager InputManager;

    [SerializeField] private Vector2 moveInput;

    [SerializeField] private Vector2 lookInput;
    private void Awake()
    {
        InputManager = GameManager.instance.InputManager;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetMoveInput(Vector2 inputValue)
    {
        moveInput = new Vector2 (inputValue.x, inputValue.y);
    }

    private void SetLookInput(Vector2 inputValue)
    {
        lookInput = new Vector2 (inputValue.x, inputValue.y);
    }

    void JumpStartedInput()
    {
        Debug.Log("Jump Started");
    }

    void JumpFinishedInput()
    {
        Debug.Log("Jump Finished");
    }

    void JumpCancelledInput()
    {
        Debug.Log("Jump Cancelled");
    }

    private void OnEnable()
    {
        InputManager.MoveInputEvent += SetMoveInput;
        InputManager.LookInputEvent += SetLookInput;

        InputManager.JumpStartedInputEvent += JumpStartedInput;
        InputManager.JumpFinishedInputEvent += JumpFinishedInput;
        InputManager.JumpCancelledInputEvent += JumpCancelledInput;
    }


    private void OnDestroy()
    {
        InputManager.MoveInputEvent -= SetMoveInput;
        InputManager.LookInputEvent -= SetLookInput;
    }
}
