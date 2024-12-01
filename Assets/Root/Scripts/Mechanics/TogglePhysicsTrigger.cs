using UnityEngine;

public class TogglePhysicsTrigger : ToggleBehaviour<ToggleableBehaviour>
{
    // MEMBERS
    [SerializeField] public bool toggleOnTriggerEnter = true;
    [SerializeField] public bool toggleOnTriggerExit = false;
    [SerializeField] protected string[] tags = { Const.Tags.Player };
    [SerializeField] protected bool invertToggle;

    // METHODS
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (toggleOnTriggerEnter)
        {
            foreach (var tag in tags)
            {
                if (other.tag == tag)
                {
                    Toggle();
                }
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (toggleOnTriggerExit)
        {
            foreach (var tag in tags)
            {
                if (other.tag == tag)
                {
                    Toggle();
                }
            }
        }
    }

}
