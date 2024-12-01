using UnityEngine;

public class ToggleableSetObjectStates : ToggleableBehaviour
{
    // MEMBERS
    [SerializeField] protected Behaviour[] behaviours;
    [SerializeField] protected GameObject[] gameObjects;

    // PROPERTIES
    public Behaviour[] Behaviours => behaviours;
    public GameObject[] GameObjects => gameObjects;

    // METHODS
    public override void Toggle()
    {
        base.Toggle();
        SetObjectStates();
    }

    protected virtual void SetObjectStates()
    {
        foreach (var gameObject in gameObjects)
        {
            gameObject.SetActive(isToggled);
        }
        foreach (var behaviour in behaviours)
        {
            behaviour.enabled = isToggled;
        }
    }

}
