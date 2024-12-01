public delegate void ITriggerableEvent();

public interface ITriggerable
{
    // PROPERTIES
    bool IsTriggered { get; }

    // EVENTS
    event ITriggerableEvent OnTriggerCallback;

    // METHODS
    void Trigger();
}
