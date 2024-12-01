using UnityEngine;

public class BasicCameraFollow : MonoBehaviour
{
    // MEMBERS
    [SerializeField] protected Transform target;
    [SerializeField] new protected Camera camera;

    [Header("Camera Settings")]
    [Range(0f, 1f)]
    [SerializeField] protected float positionDamp = 0.4f;
    [SerializeField] protected bool autoDeltaPosition = true;
    [SerializeField] protected Vector3 deltaPosition;
    [Tooltip("Enable this if target is being moved with physics")]
    [SerializeField] protected bool isFollowingPhysicsObject;
    [Header("Follow Target Relative Position")]
    [SerializeField] protected bool trackGroundPosition;
    [SerializeField] protected float maxTrackGroundDistance = 10f;
    [SerializeField] protected int[] layers = { Const.Layers.DefaultIndex, Const.Layers.FloorIndex };
    [Range(0f, 1f)]
    [SerializeField] protected float groundDamp = 0.1f;
    private Vector3 velocity;
    private Vector3 groundPosition;

    // PROPERTIES
    public Transform    Target          => target;
    public Camera       Camera          => camera;
    public Vector3      DeltaPosition   => deltaPosition;
    public int[]        Layers          => layers;

    // UNITY METHODS
    private void Awake()
    {
        if (target == null)
            throw new System.MissingMemberException("Target is null");
        if (camera == null)
            throw new System.MissingMemberException("Camera is null");

        if (autoDeltaPosition)
        {
            deltaPosition = transform.position - target.position;
        }

        // Run through 1 seconds worth of Updates to stabilize camera
        for (int i = 0; i < 60; i++)
        {
            UpdateCameraPosition();
        }
    }

    private void FixedUpdate()
    {
        if (isFollowingPhysicsObject)
        {
            UpdateCameraPosition();
        }
    }

    private void LateUpdate()
    {
        if (!isFollowingPhysicsObject)
        {
            UpdateCameraPosition();
        }
    }

    private void OnValidate()
    {
        if (camera == null)
        {
            // Get all availbale cameras
            var cameras = FindObjectsOfType<Camera>();

            // if only one, take it
            if (cameras.Length == 1)
            {
                camera = cameras[0];
            }
            // if multiple, take main camera
            // if none, loop does not occur
            else
            {
                foreach (var camera in cameras)
                {
                    if (camera.tag == Const.Tags.MainCamera)
                    {
                        this.camera = camera;
                    }
                }
            }
        }
    }

    // METHODS
    public void SetDeltaPosition(Vector3 delta)
    {
        deltaPosition = delta;
    }

    public void SetRotation(Vector3 rotation)
    {
        var quaternion = Quaternion.Euler(rotation);
        deltaPosition = quaternion * deltaPosition;
        transform.rotation = quaternion;
    }

    public void AddRotation(Vector3 rotation)
    {
        var quaternion = Quaternion.Euler(rotation);
        quaternion = transform.rotation * quaternion;
        deltaPosition = quaternion * deltaPosition;
        transform.rotation = quaternion;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private Vector3? GetGroundPosition()
    {
        var origin = target.position;
        var direction = Vector3.down;

        // Compute layer mask
        var layerMask = 0;
        foreach (var layer in layers)
        {
            layerMask += 1 << layer;
        }

        RaycastHit info;
        Physics.Raycast(origin, direction, out info, maxTrackGroundDistance, layerMask);

        if (info.collider == null)
        {
            return null;
        }
        else
        {
            return info.point;
        }
    }

    private void UpdateCameraPosition()
    {
        if (target == null)
        {
            var defaultfrom = transform.position;
            var defaultTo = deltaPosition;
            transform.position = Vector3.SmoothDamp(defaultfrom, defaultTo, ref velocity, positionDamp);

            // stop execution here
            return;
        }

        // Calc position below player
        var groundRayPosition = GetGroundPosition();
        var hitGround = groundRayPosition != null;
        var groundTo = (hitGround) ? (Vector3)groundRayPosition : Vector3.zero;
        groundPosition = Vector3.Lerp(groundPosition, groundTo, groundDamp);

        // Calc position for camera
        var from = transform.position;
        var to = (trackGroundPosition && hitGround)
            ? groundPosition + deltaPosition
            : target.position + deltaPosition;
        transform.position = Vector3.SmoothDamp(from, to, ref velocity, positionDamp);
    }

}
