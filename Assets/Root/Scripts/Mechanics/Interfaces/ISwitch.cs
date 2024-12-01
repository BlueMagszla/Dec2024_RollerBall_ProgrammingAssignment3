public delegate void ISwitchEvent(bool state);

public interface ISwitch<Switchable>
    where Switchable : ISwitchable
{
    // PROPERTIES
    Switchable[] Switchables { get; }

    // EVENTS
    event ISwitchEvent OnSwitcherCallback;

    // METHODS
    void Switch(bool state);
}
