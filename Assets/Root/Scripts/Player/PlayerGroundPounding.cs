using UnityEngine;

public class PlayerGroundPounding : PlayerStateBehaviour
{
    public override void OnStateSetup()
    {
        // nothing
    }

    public override void OnStateEnter()
    {
        // Force the player down towards the ground
        var force = Vector3.down * PlayerParams.GroundPoundForce;
        Rigidbody.angularVelocity = Vector3.zero;
        Rigidbody.AddForce(force, ForceMode.Impulse);
    }

    public override void OnStateUpdate()
    {
        // nothing
    }

    public override void OnStateExit()
    {
        Rigidbody.angularVelocity = Vector3.zero;
    }

    // Exits state on collision with ground
    private void OnCollisionEnter(Collision collision) => HandleGroundCollision(collision);
}
