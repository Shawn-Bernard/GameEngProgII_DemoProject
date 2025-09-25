using UnityEngine;

public interface IState 
{
    
    void EnterState();

    void FixedUpdateState();

    void UpdateState();

    void LateUpdateState();

    void ExitState();
    
}
