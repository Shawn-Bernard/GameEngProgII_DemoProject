using UnityEngine;

public interface IInteractable 
{
    void OnInteract();

    public string GetInteractionPromt();

    void SetFocus(bool focused);


}
