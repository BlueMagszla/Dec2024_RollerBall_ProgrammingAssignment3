using UnityEngine;

public class PlayerDashing : PlayerStateBehaviour
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
        // PRIORITY 1 - Exit State
        if (StateEnterTime + PlayerParams.DashMinDuration < Time.time)
        {
            ChangeState(PlayerFsmEvents.SpeedSlowed);
        }
        else
        {
            // Add force to player
            var force = PlayerEntity.Axis * PlayerParams.MoveSpeed;
            Rigidbody.AddForce(force, ForceMode.Force);
        }
    }

    public override void OnStateExit()
    {
        // nothing
    }

}
