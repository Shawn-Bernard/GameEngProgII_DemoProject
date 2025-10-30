using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Outline))]
public abstract class BaseInteractable : MonoBehaviour, IInteractable
{
    [Header("Outline/Highlight Settings")]
    protected Outline Outline;

    protected bool isFocused = false;

    

    public void Awake()
    {
        if (gameObject.layer != LayerMask.NameToLayer("Interactable"))
        {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
        }
        #region Initalize Highlight Effect

        Outline = GetComponent<Outline>();
        Outline.OutlineColor = Color.green;
        Outline.OutlineWidth = 10f;
        Outline.enabled = true;

        #endregion
    }
    public string GetInteractionPromt()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnInteract()
    {
        Debug.Log("On interact activated");
    }

    public void SetFocus(bool focused)
    {
        if (isFocused == focused)
        { 
            return; 
        }

        isFocused = focused;

        if (focused)
        {
            // Perform focused logic
            Outline.enabled = true;
        }
        else
        {
            // Lost focused logic
            Outline.enabled = false;
        }
    }

    
}
