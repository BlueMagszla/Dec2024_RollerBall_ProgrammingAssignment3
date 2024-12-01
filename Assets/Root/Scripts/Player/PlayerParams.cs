using UnityEngine;

[CreateAssetMenu]
public class PlayerParams : ScriptableObject
{
    private const string groudPoundDotProductMinTooltip =
        "Minimum Dot-Product value to enter state.\n\n" +
        "Value of +1: can ground pound at any time (velocity is at or below Vector3.Up).\n\n" +
        "Value of -1: can ground pound only when velocity is directly down (Vector3.Down)";

    // CONTROLLER
    [Header("Controller Input")]
    [SerializeField] protected KeyCode[] dashKeys   = { KeyCode.LeftShift, KeyCode.RightShift, KeyCode.P };
    [SerializeField] protected KeyCode[] jumpKeys   = { KeyCode.Space, KeyCode.L };
    [SerializeField] protected KeyCode[] leftKeys   = { KeyCode.A, KeyCode.LeftArrow };
    [SerializeField] protected KeyCode[] rightKeys  = { KeyCode.D, KeyCode.RightArrow };
    [SerializeField] protected KeyCode[] upKeys     = { KeyCode.W, KeyCode.UpArrow };
    [SerializeField] protected KeyCode[] downKeys   = { KeyCode.S, KeyCode.DownArrow };

    // CHARACTER STATES
    [Header("General")]
    [SerializeField] protected int maxHealth                        = 3;
    [SerializeField] protected int attackPower                      = 1;
    [SerializeField] protected float attackKnockback                = 5f;
    [SerializeField] protected float damageKnockback                = 5f;
    [SerializeField] protected float damageFlashDuration            = .3f;
    [SerializeField] protected float restingDrag                    = 1f;
    [SerializeField] protected float groundingRaycastLength         = 0.6f;
    [SerializeField] protected int[] groundLayers                   = { Const.Layers.DefaultIndex, Const.Layers.FloorIndex };
    [Header("Idle")]
    [SerializeField] protected float minIdleVelocity                = 2f;
    [Header("Move")]
    [SerializeField] protected float moveSpeed                      = 5f;
    [Header("Jump")]
    [SerializeField] protected float jumpForce                      = 10f;
    [SerializeField] protected float jumpSpeed                      = 2f;
    [SerializeField] protected MinMaxFloat jumpForceVelocityScaler  = new MinMaxFloat(0.5f, 1f, 0f, 1f, false);
    [Range(0f, 90f)]
    [SerializeField] protected float jumpForceAngle                 = 90f;
    [Header("Ground Pound")]
    [Range(-1f, 1f)]
    [Tooltip(groudPoundDotProductMinTooltip)]
    [SerializeField] protected float groundPoundDotProductMin       = 0f;
    [SerializeField] protected float groundPoundForce               = 10f;
    [Header("Charge Dash")]
    [SerializeField] protected float maxDashChargeSeconds           = 3f;
    [SerializeField] protected float dashMinDuration                = .25f;
    [SerializeField] protected float dashSpeed                      = 10f;
    [SerializeField] protected AnimationCurve dashCurve             = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] protected float chargeDashRotationsPerSecond   = 5f;

    // Keys
    public KeyCode[] DashKeys   => dashKeys;
    public KeyCode[] JumpKeys   => jumpKeys;
    public KeyCode[] LeftKeys   => leftKeys;
    public KeyCode[] RightKeys  => rightKeys;
    public KeyCode[] UpKeys     => upKeys;
    public KeyCode[] DownKeys   => downKeys;

    // General
    public int MaxHealth                        => maxHealth;
    public int AttackPower                      => attackPower;
    public float AttackKnockback                => attackKnockback;
    public float DamageKnockback                => damageKnockback;
    public float DamageFlashDuration            => damageFlashDuration;
    public float RestingDrag                    => restingDrag;
    public float JumpRaycast                    => groundingRaycastLength;
    public int[] GroundLayers                   => groundLayers;
    // Idle
    public float MinVelocityIdle                => minIdleVelocity;
    // Move
    public float MoveSpeed                      => moveSpeed;
    // Jump
    public float JumpForce                      => jumpForce;
    public float JumpMoveSpeed                  => jumpSpeed;
    public MinMaxFloat JumpForceVelocityScaler  => jumpForceVelocityScaler;
    public float JumpForceAngle                 => jumpForceAngle;
    // Ground Pound
    public float GroundPoundDotProductMin       => groundPoundDotProductMin;
    public float GroundPoundForce               => groundPoundForce;
    // Charge Dash
    public float MaxSpinDashChargeSeconds       => maxDashChargeSeconds;
    public float DashMinDuration                => dashMinDuration;
    public float DashSpeed                      => dashSpeed;
    public AnimationCurve DashCurve             => dashCurve;
    public float ChargeDashRotationsPerSecond   => chargeDashRotationsPerSecond;


}