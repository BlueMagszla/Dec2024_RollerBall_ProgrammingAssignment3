using UnityEngine;

public class PlayerIdling : PlayerStateBehaviour
{
    private float drag;

    public override void OnStateSetup()
    {
        // nothing
    }

    public override void OnStateEnter()
    {
        // Store drag value
        drag = Rigidbody.drag;
        // Assign new drag value
        Rigidbody.drag = PlayerParams.RestingDrag;
    }

    public override void OnStateUpdate()
    {
        // PRIORITY 1 - Jump
        if (PlayerEntity.IsJumpDown && PlayerEntity.IsGrounded)
        {
            ChangeState(PlayerFsmEvents.JumpRegistered);
        }
        // PRIORITY 2 - Spin Dash
        else if (PlayerEntity.IsChargeDashDown && PlayerEntity.IsGrounded)
        {
            ChangeState(PlayerFsmEvents.ChargeDash);
        }
        // PRIORITY 3 - Move
        // This can be triggered if we receive forces / gravity
        else if (PlayerEntity.Axis.magnitude > 0
            || Rigidbody.velocity.magnitude > PlayerParams.MinVelocityIdle)
        {
            ChangeState(PlayerFsmEvents.MoveRegistered);
        }
    }

    public override void OnStateExit()
    {
        // Reset drag value
        Rigidbody.drag = drag;
    }

}
