using UnityEngine;

public class PlayerJumping : PlayerStateBehaviour
{
    public override void OnStateSetup()
    {
        // nothing
    }

    public override void OnStateEnter()
    {
        // Calculate relative jump force
        var min = PlayerParams.JumpForceVelocityScaler.min;
        var max = PlayerParams.JumpForceVelocityScaler.max;
        var velocityMagnitude = Rigidbody.velocity.magnitude / PlayerParams.MoveSpeed;
        var relativeVelocity = Mathf.Clamp(velocityMagnitude, min, max);
        // Calculate jump vector
        var force = Vector3.up * PlayerParams.JumpForce * relativeVelocity;
        // Clear existing velocity.y
        var velocity = Rigidbody.velocity;
        velocity.y = 0f;
        Rigidbody.velocity = velocity;
        // Apply new force
        Rigidbody.AddForce(force, ForceMode.Impulse);
    }

    public override void OnStateUpdate()
    {
        // PRIORITY 1 - Ground Pound
        if (PlayerEntity.IsJump)
        {
            var velocityClamp = Mathf.Clamp01(1f / Rigidbody.velocity.magnitude);
            var velocityUnitVector = Rigidbody.velocity.normalized;
            var relativeVector = velocityUnitVector * (1f - velocityClamp);

            var dotProduct = Vector3.Dot(relativeVector, Vector3.up);

            // If the dot product is negative, we are falling
            if (dotProduct < PlayerParams.GroundPoundDotProductMin)
            {
                ChangeState(PlayerFsmEvents.GroundPoundRegistered);
            }
        }
        // PRIORITY 2 - Move
        else
        {
            // Add force to player
            var force = PlayerEntity.Axis * PlayerParams.JumpMoveSpeed;
            Rigidbody.AddForce(force, ForceMode.Force);
        }
    }

    public override void OnStateExit()
    {
        // nothing
    }

    private void OnCollisionEnter(Collision collision) => HandleGroundCollision(collision);
}
