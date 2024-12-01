public delegate void IToggleEvent();

public interface IToggle<Toggleable>
    where Toggleable : IToggleable
{
    // PROPERTIES
    Toggleable[] Toggleables { get; }

    // EVENTS
    event IToggleEvent OnToggleCallback;

    // METHODS
    void Toggle();
}
