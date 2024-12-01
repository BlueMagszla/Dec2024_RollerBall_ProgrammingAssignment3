public delegate void ISwitchableEvent(bool state);

public interface ISwitchable
{
    // PROPERTIES
    bool IsSwitched { get; }

    // EVENTS
    event ISwitchableEvent OnSwitchableCallback;

    // METHODS
    void Switch(bool isSwitched);
}
