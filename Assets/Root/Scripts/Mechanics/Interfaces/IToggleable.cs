public delegate void IToggleableEvent(bool isToggled);

public interface IToggleable
{
    // PROPERTIES
    bool IsToggled { get; }

    // EVENTS
    event IToggleableEvent OnToggleableCallback;

    // METHODS
    void Toggle();
}
