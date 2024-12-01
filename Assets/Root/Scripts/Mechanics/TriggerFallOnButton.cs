using UnityEngine;

public class TriggerFallOnButton : TriggerBehaviour<TriggerableBehaviour>
{
    // MEMBERS
    [SerializeField] protected string[] tags = { Const.Tags.Player };

    // PROPERTIES
    public string[] Tags => tags;

    // MEMBERS
    protected void OnTriggerEnter(Collider other)
    {
        var rigidbody = other.GetComponentInParent<Rigidbody>();
        if (rigidbody == null)
            return; // kill routine

        foreach (var tag in tags)
        {
            if (tag == other.tag)
            {
                var isFalling = Vector3.Dot(rigidbody.velocity, Vector3.down) < 0;

                if (isFalling)
                {
                    Trigger();
                    break; // quit foreach loop
                }
            }
        }
    }
}
