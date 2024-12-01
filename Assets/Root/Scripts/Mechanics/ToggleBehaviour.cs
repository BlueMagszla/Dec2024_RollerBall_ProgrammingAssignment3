using System.Collections.Generic;
using UnityEngine;

public abstract class ToggleBehaviour<Toggleable> : MonoBehaviour, IToggle<Toggleable>
    where Toggleable : IToggleable
{
    // MEMBERS
    [SerializeField] protected List<Toggleable> toggleables;

    // PROPERTIES
    public Toggleable[] Toggleables => toggleables.ToArray();

    // EVENTS
    public event IToggleEvent OnToggleCallback;

    // METHODS
    public virtual void Toggle()
    {
        foreach (var toggle in toggleables)
        {
            toggle.Toggle();
        }

        OnToggleCallback?.Invoke();
    }
}
