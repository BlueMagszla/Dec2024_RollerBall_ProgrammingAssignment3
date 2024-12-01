public delegate void ITriggerEvent();

public interface ITrigger<Triggerable>
    where Triggerable : ITriggerable
{
    // PROPERTIES
    Triggerable[] Triggerables { get; }

    // EVENTS
    event ITriggerEvent OnTriggerCallback;

    // METHODS
    void Trigger();
}
