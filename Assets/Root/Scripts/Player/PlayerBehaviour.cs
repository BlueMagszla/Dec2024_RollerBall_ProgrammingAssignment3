using UnityEngine;

public abstract class PlayerStateBehaviour : MonoBehaviour
{
    // MEMBERS
    [SerializeField]
    private bool debugState = false;
    [SerializeField]
    private PlayerEntity playerEntity;

    // PROPERTIES
    public float StateEnterTime { get; private set; } = 0;

    // PROPERTIES WITH SERIALIZED VALUES
    public PlayerEntity PlayerEntity => playerEntity;
    public PlayerParams PlayerParams => playerEntity.Params;
    public Rigidbody Rigidbody => playerEntity.Rigidbody;

    // METHODS
    protected virtual void Awake()
    {
        OnStateSetup();
    }

    protected virtual void OnEnable()
    {
        if (debugState)
        {
            Debug.LogFormat("{0} entered the state {1}", name, GetType().Name);
        }

        // Record time we entered this state
        StateEnterTime = Time.time;

        OnStateEnter();
    }

    protected virtual void FixedUpdate()
    {
        OnStateUpdate();
    }

    protected virtual void OnDisable()
    {
        if (debugState)
        {
            Debug.LogFormat("{0} exited the state {1}", name, GetType().Name);
        }

        OnStateExit();
    }

    // ABSTRACT METHODS
    // An abstract method is a signature (method name, return type, and parameters)
    // that needs to be implemented by the inheriting class.
    public abstract void OnStateSetup();
    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();

    // 
    protected void HandleGroundCollision(Collision collision)
    {
        var collisionLayer = 1 << collision.gameObject.layer;

        foreach (var groundLayer in PlayerParams.GroundLayers)
        {
            var layer = 1 << groundLayer;
            var hitGround = (collisionLayer & layer) != 0;
            if (hitGround)
            {
                ChangeState(PlayerFsmEvents.Grounded);
                break;
            }
        }
    }

    // Fancy auto-serialization logic
    public void OnValidate()
    {
        if (playerEntity == null)
            playerEntity = GetComponentInChildren<PlayerEntity>();
        if (playerEntity == null)
            playerEntity = GetComponentInParent<PlayerEntity>();
    }

    public void ChangeState(PlayerFsmEvents transition)
    {
        // Debug "emulation" layer
        //var activeStateName = playerEntity.PlayerFSM.ActiveState.GetType().Name;
        //Debug.Log($"State {activeStateName} requested transition {transition}");

        playerEntity.PlayerFSM.ChangeState(transition);
    }

}
