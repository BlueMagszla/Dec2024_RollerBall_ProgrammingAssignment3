using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerBehaviour<Triggerable> : MonoBehaviour, ITrigger<Triggerable>
    where Triggerable : ITriggerable
{
    // MEMBERS
    [SerializeField] protected List<Triggerable> triggerables;

    // EVENTS
    public event ITriggerEvent OnTriggerCallback;

    // PROPERTIES
    Triggerable[] ITrigger<Triggerable>.Triggerables => triggerables.ToArray();

    // METHODS
    public void Trigger()
    {
        foreach (var trigger in triggerables)
        {
            trigger.Trigger();
        }

        OnTriggerCallback?.Invoke();
    }

}
