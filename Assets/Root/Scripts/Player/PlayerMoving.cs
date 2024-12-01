using UnityEngine;

public class PlayerMoving : PlayerStateBehaviour
{
    public override void OnStateSetup()
    {
        // nothing
    }

    public override void OnStateEnter()
    {
        // nothing
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
        else if (PlayerEntity.IsAxisActive)
        {
            // Add force to player
            var force = PlayerEntity.Axis * PlayerParams.MoveSpeed;
            Rigidbody.AddForce(force, ForceMode.Force);
        }
        // PRIORITY 4 - Check for Idling
        // Check if velocity is below threshold
        // if it is, go to idle state via transition
        else if (Rigidbody.velocity.magnitude < PlayerParams.MinVelocityIdle)
        {
            ChangeState(PlayerFsmEvents.SpeedSlowed);
        }
    }

    public override void OnStateExit()
    {
        // nothing
    }

}
