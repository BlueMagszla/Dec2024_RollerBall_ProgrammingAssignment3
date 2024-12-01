// Small "emulation" layer to replace BehaviourMachine.
// Certainly not perfect, but gets the job done.

using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFSM : MonoBehaviour
{
    public int initialStateIndex;
    public State[] fsmStates;
    private State activeState;

    public PlayerStateBehaviour ActiveState => activeState.behaviour;

    private void Start()
    {
        // make sure to turn off all state scripts
        foreach (var fsmState in fsmStates)
        {
            fsmState.behaviour.enabled = false;
        }

        activeState = fsmStates[initialStateIndex];
        activeState.behaviour.enabled = true;
    }

    public void ChangeState(PlayerFsmEvents fsmEvent)
    {
        foreach (var transition in activeState.transitions)
        {
            var canTransition = fsmEvent == transition.fsmEvent;
            if (canTransition)
            {
                activeState.behaviour.enabled = false;
                activeState = GetStateFromBehaviour(transition.state);
                activeState.behaviour.enabled = true;
            }
        }
    }

    private State GetStateFromBehaviour(PlayerStateBehaviour behaviour)
    {
        foreach (var fsmState in fsmStates)
        {
            if (fsmState.behaviour == behaviour)
            {
                return fsmState;
            }
        }

        throw new System.ArgumentOutOfRangeException();
    }
}

[System.Serializable]
public struct State
{
    public string name;
    [FormerlySerializedAs("state")]
    public PlayerStateBehaviour behaviour;
    public StateTransition[] transitions;
}

[System.Serializable]
public struct StateTransition
{
    [FormerlySerializedAs("eventKey")]
    public PlayerFsmEvents fsmEvent;
    [FormerlySerializedAs("state")]
    public PlayerStateBehaviour state;
}

public enum PlayerFsmEvents
{
    ReleaseDash,
    ChargeDash,
    JumpRegistered,
    MoveRegistered,
    GroundPoundRegistered,
    Grounded,
    SpeedSlowed,
    Died,
}