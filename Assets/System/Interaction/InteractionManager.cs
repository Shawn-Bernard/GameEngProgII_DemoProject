using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [Header("Interaction Settings")]
    private LayerMask interactableLayer;
    [SerializeField] private float interactionDistance = 5f;

    private IInteractable currentFocusedInteractable;

    private GameObject cameraRoot;

    [Header("Interaction cooldown")]
    private float interactionCooldown = 0.1f;
    private float lastInteractionTime = -Mathf.Infinity;

    private InputManager inputManager => GameManager.instance.InputManager;
    private void Start()
    {
        interactableLayer = LayerMask.GetMask("Interactable");

        cameraRoot = GameManager.instance.PlayerController.cameraRoot;
    }

    private void Update()
    {
        HandleInteractionDetection();
    }

    // Moving to states 
    private void HandleInteractionDetection()
    {
        if (Physics.Raycast(cameraRoot.transform.position,cameraRoot.transform.forward,out RaycastHit hitInfo, interactionDistance, interactableLayer))
        {
            Debug.Log("Raycast hit >> " + hitInfo.collider.gameObject.name);

            // Get the interactable component from hit object 
            IInteractable hitInteractable = hitInfo.collider.GetComponent<IInteractable>();

            if (hitInteractable != null)
            {
                if (hitInteractable !=  currentFocusedInteractable)
                {
                    if (currentFocusedInteractable != null)
                    currentFocusedInteractable.SetFocus(false);
                }

                //currentFocusedInteractable = hitInteractable;

                //currentFocusedInteractable.SetFocus(true);

                Debug.LogWarning($"Object {hitInfo.collider.gameObject.name} has no interactable component ");


            }

            currentFocusedInteractable = hitInteractable;

            currentFocusedInteractable.SetFocus(true);
        }
        else if (currentFocusedInteractable != null) 
        {
            currentFocusedInteractable.SetFocus(false);
            currentFocusedInteractable = null;
        }
    }

    private void OnInteractInput(InputAction.CallbackContext context)
    {
        if (Time.time - interactionCooldown != lastInteractionTime)
        if (context.performed)
        {
            if (currentFocusedInteractable != null)
            {
                currentFocusedInteractable.OnInteract();
            }
        }
    }

    private void OnEnable()
    {
        inputManager.InteractInputEvent += OnInteractInput;
    }

    private void OnDisable()
    {
        inputManager.InteractInputEvent -= OnInteractInput;
    }
}
