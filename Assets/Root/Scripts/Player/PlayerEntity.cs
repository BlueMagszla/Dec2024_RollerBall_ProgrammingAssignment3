using System;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    // MEMBERS
    [SerializeField] protected PlayerParams playerParams;
    [SerializeField] new protected Rigidbody rigidbody;
    [SerializeField] new protected Camera camera;
    [SerializeField] protected PlayerFSM playerFSM;
    [SerializeField] protected PlayerHurtBox hurtBox;

    // PROPERTIES
    public PlayerParams Params => playerParams;
    public Rigidbody Rigidbody => rigidbody;
    public PlayerFSM PlayerFSM => playerFSM;
    public PlayerHurtBox HurtBox => hurtBox;

    public bool IsGrounded
    {
        get
        {
            var origin = transform.position;
            var direction = Vector3.down;
            var maxDistance = playerParams.JumpRaycast;

            // Compute layer mask
            var layerMask = 0;
            foreach (var layer in Params.GroundLayers)
            {
                layerMask += 1 << layer;
            }

            RaycastHit info;
            Physics.Raycast(origin, direction, out info, maxDistance, layerMask);
            var isGrounded = info.collider != null;

            Debug.DrawLine(origin, origin + direction * maxDistance, Color.red);

            return isGrounded;
        }
    }

    // Jump input
    public bool IsJump              => GetAnyKey(playerParams.JumpKeys);
    public bool IsJumpDown          => GetAnyKeyDown(playerParams.JumpKeys);
    public bool IsJumpUp            => GetAnyKeyUp(playerParams.JumpKeys);
    // Dash input
    public bool IsChargeDash        => GetAnyKey(Params.DashKeys);
    public bool IsChargeDashDown    => GetAnyKeyDown(Params.DashKeys);
    public bool IsChargeDashUp      => GetAnyKeyUp(Params.DashKeys);
    // X/Z input
    public Vector3 Axis
    {
        get
        {
            // Get -x, +x, -y, +y
            var horizontalPos   = GetAnyKey(Params.RightKeys);
            var horizontalNeg   = GetAnyKey(Params.LeftKeys);
            var verticalPos     = GetAnyKey(Params.UpKeys);
            var verticalNeg     = GetAnyKey(Params.DownKeys);

            // Combine Xs and Ys
            var horizontal      = (horizontalPos ? +1 : 0) + (horizontalNeg ? -1 : 0);
            var vertical        = (verticalPos   ? +1 : 0) + (verticalNeg   ? -1 : 0);

            // Create normalized axes vector
            var axis = new Vector3(horizontal, 0f, vertical);
            axis.Normalize();

            // Rotate axis about camera Y axis
            // This makes up/forward always orthogonal to the camera
            if (camera != null)
            {
                axis = Quaternion.Euler(0, camera.transform.rotation.eulerAngles.y, 0) * axis;
            }

            return axis;
        }

    }
    public bool IsAxisActive
    {
        get
        {
            return Axis.magnitude > 0.5f;
        }
    }

    // METHODS
    /// <summary>
    /// Check multiple key inputs at once
    /// </summary>
    /// <param name="keys"></param>
    /// <param name="inputFunction"></param>
    /// <returns></returns>
    private bool KeyCodeFunc(KeyCode[] keys, Func<KeyCode, bool> inputFunction)
    {
        foreach (var key in keys)
        {
            if (inputFunction(key))
            {
                return true;
            }
        }
        return false;
    }
    private bool GetAnyKey(KeyCode[] keys) => KeyCodeFunc(keys, Input.GetKey);
    private bool GetAnyKeyUp(KeyCode[] keys) => KeyCodeFunc(keys, Input.GetKey);
    private bool GetAnyKeyDown(KeyCode[] keys) => KeyCodeFunc(keys, Input.GetKey);

    // Handy automatic serialization
    private void OnValidate()
    {
        if (rigidbody == null)
            rigidbody = GetComponentInChildren<Rigidbody>();
        if (rigidbody == null)
            rigidbody = GetComponentInParent<Rigidbody>();
    }
}
