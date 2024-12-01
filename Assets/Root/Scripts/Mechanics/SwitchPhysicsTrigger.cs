using UnityEngine;

public class SwitchPhysicsTrigger : SwitchBehaviour
{
    // MEMBERS
    [SerializeField] public bool switchOnTriggerEnter = true;
    [SerializeField] public bool switchOnTriggerExit = false;
    [SerializeField] protected string[] tags = { Const.Tags.Player };
    [SerializeField] protected bool switchToState = true;

    // METHODS
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (switchOnTriggerEnter)
        {
            foreach (var tag in tags)
            {
                if (other.tag == tag)
                {
                    Switch(switchToState);
                }
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (switchOnTriggerExit)
        {
            foreach (var tag in tags)
            {
                if (other.tag == tag)
                {
                    Switch(switchToState);
                }
            }
        }
    }
}
