using UnityEngine;

public class ToggleableBehaviour : MonoBehaviour, IToggleable
{
    // MEMBERS
    [SerializeField] protected bool invertToggle;
    protected bool isToggled;

    // PROPERTIES
    public bool IsToggled => isToggled;

    // EVENTS
    public event IToggleableEvent OnToggleableCallback;

    // METHODS
    protected virtual void Awake()
    {
        isToggled = invertToggle;

        // initialize/sync listener values
        OnToggleableCallback?.Invoke(isToggled);
    }

    public virtual void Toggle()
    {
        isToggled = !isToggled; // toggle
        OnToggleableCallback?.Invoke(isToggled);
    }
}
