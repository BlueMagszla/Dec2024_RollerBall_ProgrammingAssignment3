using UnityEngine;

public class PlayerChargingDash : PlayerStateBehaviour
{
    protected float drag;
    protected float chargeSeconds;
    protected Vector3 lastAxis;

    public override void OnStateSetup()
    {
        // nothing
    }

    public override void OnStateEnter()
    {
        // Zero out rotation for animation
        Rigidbody.MoveRotation(Quaternion.identity);
        // Zero out velocity
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
        // Record drag
        drag = Rigidbody.drag;
        // Set drag to "calm" rigidbody
        Rigidbody.drag = PlayerParams.RestingDrag;
    }

    public override void OnStateUpdate()
    {
        // Animate player spin
        if (PlayerEntity.IsAxisActive)
        {
            // If new input, reset rotation so we don't start copounding rotations
            var input = PlayerEntity.Axis;
            if (input != lastAxis)
            {
                Rigidbody.MoveRotation(Quaternion.identity);
                lastAxis = input;
            }
        }
        var animRotationSpeed = PlayerParams.ChargeDashRotationsPerSecond;
        var crossProduct = Vector3.Cross(lastAxis, Vector3.down);
        var chargeScaler = chargeSeconds / PlayerParams.MaxSpinDashChargeSeconds;
        var rotationEuler = animRotationSpeed * 360f * Time.deltaTime * chargeScaler;
        var rotation = Rigidbody.rotation * Quaternion.Euler(crossProduct * rotationEuler);
        Rigidbody.MoveRotation(rotation);

        // PRIORITY 1 - Exit State
        if (!PlayerEntity.IsChargeDash && PlayerEntity.IsGrounded && PlayerEntity.IsAxisActive)
        {
            ChangeState(PlayerFsmEvents.ReleaseDash);
        }
        // PRIORITY 2 - Jump
        // This bypasses adding force
        else if (PlayerEntity.IsJump)
        {
            ChangeState(PlayerFsmEvents.JumpRegistered);
        }
        // PRIORITY 3 - Charge Dash
        else
        {
            // Charge dash by time delta
            chargeSeconds += Time.deltaTime;
            // Clamp value if it exceeds our threshold
            chargeSeconds = Mathf.Clamp(chargeSeconds, 0, PlayerParams.MaxSpinDashChargeSeconds);
        }
    }

    public override void OnStateExit()
    {
        // Apply stored drag
        Rigidbody.drag = drag;

        // Apply dash speed
        var curve = PlayerParams.DashCurve;
        var max = PlayerParams.MaxSpinDashChargeSeconds;
        var dashForceScaler = curve.Evaluate(chargeSeconds / max);
        var direction = PlayerEntity.Axis;
        var force = direction * PlayerParams.DashSpeed * dashForceScaler;
        // Reset velocities then add force
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.angularVelocity = Vector3.zero;
        Rigidbody.AddForce(force, ForceMode.Impulse);

        // Release charge
        chargeSeconds = 0f;
    }

}
