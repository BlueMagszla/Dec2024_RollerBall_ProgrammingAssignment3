using UnityEngine;

public abstract class TriggerableBehaviour : MonoBehaviour, ITriggerable
{
    // MEMBERS
    protected bool isTriggered = true;

    // PROPERTIES
    bool ITriggerable.IsTriggered => isTriggered;

    // EVENTS
    public event ITriggerableEvent OnTriggerCallback;

    // METHODS
    public virtual void Trigger()
    {
        OnTriggerCallback?.Invoke();
    }
}
