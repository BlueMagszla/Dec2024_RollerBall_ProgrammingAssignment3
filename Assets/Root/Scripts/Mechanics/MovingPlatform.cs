using System.Collections.Generic;
using UnityEngine;

public sealed class MovingPlatform : MonoBehaviour
{
    internal static class Tooltips
    {
        // PATH
        public const string waypoints =
            "The transforms to use as waypoints for the platform's path. Waypoints are followed in list order.";

        public const string startIndex =
            "Which waypoint index to start at.";

        public const string pathIsCircuit =
            "If true, the waypoints act as a circuit; the last waypoint leads to the first. Otherwise, the path" +
            "ping-pongs between the first and last waypoints, travelling through each along the way.";

        public const string pathIsInverted =
            "If true, the platform moves in the opposite order as listed.";

        public const string unitsPerSecond =
            "How many units (meters) this platforms moves per second.";

        public const string waypointWaitTime =
            "How long the platform waits at a waypoint before continuing.";

        // MISC
        public const string isPaused =
            "If true, the platform halts in it's position.";

        // DEBUG
        public const string gizmosColor =
            "The color used for the debug gizmos.";

        public const string gizmosSize =
            "The size of the debug gizmos.";
    }

    #region TUNABLE VARIABLES

    [Header("Path")]
    [SerializeField]
    [Tooltip(Tooltips.waypoints)]
    private List<Transform> waypoints = new List<Transform>();

    [SerializeField]
    [Tooltip(Tooltips.startIndex)]
    private int startIndex = 0;

    [SerializeField]
    [Tooltip(Tooltips.pathIsCircuit)]
    private bool pathIsCircuit = false;

    [SerializeField]
    [Tooltip(Tooltips.pathIsInverted)]
    private bool pathIsInverted = false;

    [SerializeField]
    [Tooltip(Tooltips.unitsPerSecond)]
    private float unitsPerSecond = 3f;

    // Delay field input so being over large values are not immediately registered
    // which would result in long wait times.
    [Delayed]
    [SerializeField]
    [Tooltip(Tooltips.waypointWaitTime)]
    private float waypointWaitTime = 1f;

    [Header("Misc")]
    [SerializeField]
    [Tooltip(Tooltips.isPaused)]
    private bool isPaused;

    [SerializeField]
    [Tooltip(Tooltips.pathIsInverted)]
    private bool carryRigidbodies = false;

    [Header("Debug")]
    [SerializeField]
    [Tooltip(Tooltips.gizmosColor)]
    private Color gizmosColor = Color.green;

    [SerializeField]
    [Tooltip(Tooltips.gizmosSize)]
    private float gizmosSize = 1f;

    #endregion

    #region CONSTANTS

    public const int kForwardDirection = +1;
    public const int kBackwardDirection = -1;

    #endregion

    #region PROPERTIES

    // Getters, private set
    public int CurrWaypointIndex
    {
        get;
        private set;
    }
    public int NextWaypointIndex
    {
        get;
        private set;
    }
    public int PathPingPongDirection
    {
        get;
        private set;
    }
    public float RemainingWaitTimeAtWaypoint
    {
        get;
        private set;
    }
    public int TargetWaypointIndex
    {
        get;
        private set;
    }
    public List<Rigidbody> TrackedRigidbodies
    {
        get;
    } = new List<Rigidbody>();
    public float WaypointDistance
    {
        get;
        private set;
    }
    public Vector3 WaypointFrom
    {
        get;
        private set;
    }
    public Vector3 WaypointTo
    {
        get;
        private set;
    }

    // Getters, Setters
    public bool IsPaused
    {
        get => isPaused;
        set => isPaused = value;
    }

    #endregion

    /// <summary>
    /// Unity Function: Initialized variables and logic
    /// </summary>
    private void Awake()
    {
        var usableWaypointIndex = Mathf.Clamp(startIndex, 0, waypoints.Count);
        // set platform to start position.
        transform.position = waypoints[usableWaypointIndex].position;

        TargetWaypointIndex = usableWaypointIndex;
        PathPingPongDirection = pathIsInverted ? kForwardDirection : kBackwardDirection;
        PrepareToMoveToNextWaypoint();
    }

    /// <summary>
    /// Unity Magic Function: move platform on Physics timestep to be in sync with other physics objects.
    /// </summary>
    private void FixedUpdate()
    {
        if (IsPaused)
        {
            // Return from this method, stops executing code here.
            return;
        }

        var hasDistanceToMove = WaypointDistance > 0f;
        if (hasDistanceToMove)
        {
            var moveSpeed = unitsPerSecond * Time.deltaTime;
            var positionFrom = transform.position;
            var positionTo = Vector3.MoveTowards(transform.position, WaypointTo, moveSpeed);
            transform.position = positionTo;

            // Get distance travelled this update, apply it to all adjacent rigidbodies
            var deltaPosition = positionTo - positionFrom;
            MoveAdjacentRigidbodies(deltaPosition);

            // Store remaining distance to travel, for use next update
            WaypointDistance = Mathf.Clamp(WaypointDistance - moveSpeed, 0f, float.PositiveInfinity);
        }
        else // no distance left to move for this waypoint
        {
            RemainingWaitTimeAtWaypoint -= Time.deltaTime;
            var stopWaiting = RemainingWaitTimeAtWaypoint <= 0f;
            if (stopWaiting)
            {
                PrepareToMoveToNextWaypoint();
            }
        }
    }

    /// <summary>
    /// Unity Function: Record any rigidbodies that collide with the platform so we can move them when the platform moves.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        var rigidbody = collision.rigidbody;
        var hasRigidbody = rigidbody != null;

        if (hasRigidbody)
        {
            var isNotTrackingThisRigidbody = !TrackedRigidbodies.Contains(rigidbody);
            if (isNotTrackingThisRigidbody)
            {
                TrackedRigidbodies.Add(rigidbody);
            }
        }
    }

    /// <summary>
    /// Unity Function: Remove record of any rigidbodies that leaving the platform so to stop moving them.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        var rigidbody = collision.rigidbody;
        var hasRigidbody = rigidbody != null;

        if (hasRigidbody)
        {
            var isTrackingThisRigidbody = TrackedRigidbodies.Contains(rigidbody);
            if (isTrackingThisRigidbody)
            {
                TrackedRigidbodies.Remove(rigidbody);
            }
        }
    }

    /// <summary>
    /// Unity Function: use it to draw debug information about our platform path.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (waypoints.Count == 0)
            return;

        Gizmos.color = gizmosColor;
        var gizmosSize3D = Vector3.one * gizmosSize;

        // Draw between all waypoints 0 to 1 to ... n
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            var pointA = waypoints[i].position;
            var pointB = waypoints[i + 1].position;
            Gizmos.DrawLine(pointA, pointB);
            Gizmos.DrawCube(pointA, gizmosSize3D);
        }

        // Draw cube on last waypoint to cap it off
        var pointLast = waypoints[waypoints.Count - 1].position;
        Gizmos.DrawCube(pointLast, gizmosSize3D);

        if (pathIsCircuit)
        {
            // If circuit, draw between last and first waypoints
            var pointFirst = waypoints[0].position;
            Gizmos.DrawLine(pointFirst, pointLast);
        }
    }


    /// <summary>
    /// Moves all adjacent rigidbodies (typically those on top) along to where it moves. 
    /// </summary>
    /// <param name="deltaPosition">The relative position to move rigidbodies</param>
    private void MoveAdjacentRigidbodies(Vector3 deltaPosition)
    {
        if (!carryRigidbodies)
        {
            return;
        }

        foreach (var rigidbody in TrackedRigidbodies)
        {
            var currentPosition = rigidbody.position;
            var targetPosition = currentPosition + deltaPosition;
            rigidbody.MovePosition(targetPosition);
        }
    }

    /// <summary>
    /// Get the next waypoint depending on the provided parameters
    /// </summary>
    /// <param name="index">The index at which to request the next index relative to.</param>
    private int NextIndex(int index)
    {
        if (pathIsCircuit)
        {
            var pathCircuitDirection = pathIsInverted // Evaluate 'pathIsInverted'
                ? kBackwardDirection  // If true, set direction to backwards
                : kForwardDirection;  // If false, set direction to forwards

            index += pathCircuitDirection;
            index = WrapIndex(index, waypoints.Count);
        }
        else // path is ping-pong
        {
            index += PathPingPongDirection;

            var hasReachedLastPoint = index >= waypoints.Count;
            var hasReachedFirstPoint = index < 0;

            /**/
            if (hasReachedLastPoint)
            {
                PathPingPongDirection = kBackwardDirection;
                // count-1 is last index, count-2 is before last index
                var nextIndex = waypoints.Count - 2;
                index = nextIndex;
            }
            else if (hasReachedFirstPoint)
            {
                PathPingPongDirection = kForwardDirection;
                // 0 is first index, 1 is second
                var nextIndex = 1;
                index = nextIndex;
            }
        }

        return index;
    }

    /// <summary>
    /// Sets up appropriate variables from moving to nexxt waypoint.
    /// </summary>
    private void PrepareToMoveToNextWaypoint()
    {
        // Compute which waypoints to use for movement calculations
        CurrWaypointIndex = TargetWaypointIndex;            // We now at our target, so current == target
        NextWaypointIndex = NextIndex(TargetWaypointIndex); // Get our next waypoint, store that
        TargetWaypointIndex = NextWaypointIndex;            // Save next waypoint as target

        // Get waypoint positions for movement calculations
        WaypointFrom = waypoints[CurrWaypointIndex].position;
        WaypointTo = waypoints[NextWaypointIndex].position;
        WaypointDistance = Vector3.Distance(WaypointFrom, WaypointTo);

        // Reset how long the platform will need to wait at next waypoint
        RemainingWaitTimeAtWaypoint = waypointWaitTime;
    }

    /// <summary>
    /// Wraps value <paramref name="index"/> between 0 and <paramref name="length"/>
    /// </summary>
    /// <param name="index">The index to wrap</param>
    /// <param name="length">The length at which to wrap at</param>
    private int WrapIndex(int index, int length)
    {
        // Wrap values exceeding max
        index %= length;
        // Wrap values below 0
        index = (index < 0)
            ? waypoints.Count - 1
            : index;

        return index;
    }

}