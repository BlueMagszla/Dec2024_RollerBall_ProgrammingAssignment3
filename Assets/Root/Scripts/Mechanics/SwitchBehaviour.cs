using UnityEngine;

public abstract class SwitchBehaviour : MonoBehaviour, ISwitch<SwitchableBehaviour>
{
    // MEMBERS
    [SerializeField] protected SwitchableBehaviour[] switchables;

    // PROPERTIES
    public SwitchableBehaviour[] Switchables => switchables;

    // EVENTS
    public event ISwitchEvent OnSwitcherCallback;

    // METHODS
    public void Switch(bool state)
    {
        foreach (var switchable in switchables)
        {
            switchable.Switch(state);
        }

        OnSwitcherCallback?.Invoke(state);
    }
}
