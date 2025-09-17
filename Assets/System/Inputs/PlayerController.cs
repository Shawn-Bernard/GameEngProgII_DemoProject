using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    private InputManager InputManager =>  GameManager.instance.InputManager;

    private Rigidbody rigidbody;
    CharacterController characterController => GetComponent<CharacterController>();

    [Header("Input Vectors")]
    [SerializeField] private Vector2 moveInput;

    [SerializeField] private Vector2 lookInput;

    [Header("Move & Look locks")]
    public bool canMove;
    public bool canLook;

    [SerializeField] private bool JumpEnabled = true;
    [SerializeField] private bool SprintEnabled;
    [Header("Movement Speed")]
    [SerializeField] private float currentSpeed;
    public float movementSpeed = 5;

    private float speedTransitionDuration;
    [SerializeField] private float crouchSpeed = 2.0f;
    [SerializeField] private float walkSpeed = 4.0f;
    [SerializeField] private float sprintSpeed = 7.0f;

    [Header("Mouse Sensivivity")]
    public float MouseSensivivity = 05f;
    

    [Header("Move & Look locks")]
    private float lowerLookLimit = -60;
    private float UpperLookLimit = 60;

    public GameObject CameraRoot;

    private void Awake()
    {
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void LateUpdate()
    {
        HandleLook();
    }

    public void HandleMovement()
    {
        //Step 1 Getting input direction
        Vector3 moveInputDirection = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 worldMoveDirection = transform.TransformDirection(moveInputDirection);

        //Step 2 determine move speed
        float targetSpeed = walkSpeed;

        if (SprintEnabled)
        {
            targetSpeed = sprintSpeed;
        }
        else
        {
            targetSpeed = walkSpeed;
        }
        //Step 3 smoothly interpolate current speed towards  
        float lerpSpeed = 1f - Mathf.Pow(0.01f, Time.deltaTime / speedTransitionDuration);
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, lerpSpeed);
        //Step 4 handle horizontal movement


        //Step 5 handle jumping and gravity
        Vector3 horizontalMoveDirection = worldMoveDirection * currentSpeed;


        Vector3 movement = horizontalMoveDirection;

        characterController.Move(movement * Time.deltaTime);
    }

    public void HandleLook()
    {
        float LookX = lookInput.x * MouseSensivivity * Time.deltaTime;
        float LookY = lookInput.y * MouseSensivivity * Time.deltaTime;
        transform.Rotate(Vector3.up * LookX);

        Vector3 currentAngles = CameraRoot.transform.localEulerAngles;
        float newRotationX = currentAngles.x - LookY;

        newRotationX = (newRotationX > 180) ? newRotationX - 360 : newRotationX;
        newRotationX = Math.Clamp(newRotationX, lowerLookLimit, UpperLookLimit);

        CameraRoot.transform.localEulerAngles = new Vector3(newRotationX, 0, 0);
    }

    private void SetMoveInput(Vector2 inputValue)
    {
        moveInput = new Vector2 (inputValue.x, inputValue.y);
    }

    private void SetLookInput(Vector2 inputValue)
    {
        lookInput = new Vector2 (inputValue.x, inputValue.y);
    }

    private void SetJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //Debug.Log("Jump Started");
        }
    }

    private void SetCrouchInput(InputAction.CallbackContext context)
    {
        Debug.Log("Crouch button hit");
    }

    private void SetSprintInput(InputAction.CallbackContext context)
    {
        //if (!SprintEnabled) return;

        if (context.started)
        {
            SprintEnabled = true;
        }
        else if (context.canceled)
        {
            SprintEnabled = false;
        }
            Debug.Log("Sprint button hit");
    }

    private void OnEnable()
    {
        InputManager.MoveInputEvent += SetMoveInput;
        InputManager.LookInputEvent += SetLookInput;

        InputManager.JumpInputEvent += SetJumpInput;

        InputManager.CrouchInputEvent += SetCrouchInput;

        InputManager.SprintInputEvent += SetSprintInput;
    }


    private void OnDestroy()
    {
        InputManager.MoveInputEvent -= SetMoveInput;
        InputManager.LookInputEvent -= SetLookInput;

        InputManager.JumpInputEvent -= SetJumpInput;

        InputManager.CrouchInputEvent -= SetCrouchInput;

        InputManager.SprintInputEvent -= SetSprintInput;
    }
}
