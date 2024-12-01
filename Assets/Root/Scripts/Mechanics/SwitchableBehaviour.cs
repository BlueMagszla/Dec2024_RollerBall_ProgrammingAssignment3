using UnityEngine;

public abstract class SwitchableBehaviour : MonoBehaviour, ISwitchable
{
    // MEMBERS
    [SerializeField] protected bool isSwitched;

    // PROPERTIES
    public bool IsSwitched => isSwitched;

    // EVENTS
    public event ISwitchableEvent OnSwitchableCallback;

    // METHODS
    public virtual void Switch(bool isSwitched)
    {
        this.isSwitched = isSwitched;
        OnSwitchableCallback?.Invoke(isSwitched);
    }
}
