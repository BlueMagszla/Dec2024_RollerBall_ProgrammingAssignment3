using UnityEngine;

    public class Knockback : MonoBehaviour
    {
        internal static class Tooltips
        {
            public const string forceMode =
                "The ForceMode to use when adding force on contact.";

            public const string force =
                "How much force to apply on contact.";

            public const string useObjectUpVectorAsForceDirection =
                "Override 'forceDirection' uing the GameObject's own up vector as the to apply force in.";

            public const string resetDirectionVelocity =
                "Removes velocity in the 'forceDirection' direction before applying force.";

            public const string forceDirection =
                "The direction in which to apply force on contact.";
        }

        public enum ForceDirection
        {
            collisionDirectionRelative,

            objectLocalRight,
            objectLocalLeft,
            objectLocalUp,
            objectLocalDown,
            objectLocalForward,
            objectLocalBack,

            customForceDirection,
        }

        #region VARIABLES

        [SerializeField]
        [Tooltip(Tooltips.forceMode)]
        private ForceMode forceMode = ForceMode.Impulse;

        [SerializeField]
        [Tooltip(Tooltips.force)]
        private float force = 10f;

        [SerializeField]
        [Tooltip(Tooltips.useObjectUpVectorAsForceDirection)]
        private ForceDirection forceDirection = ForceDirection.collisionDirectionRelative;

        [SerializeField]
        private bool ignoreVelocityY = false;

        [SerializeField]
        [Tooltip(Tooltips.resetDirectionVelocity)]
        private bool resetDirectionVelocity = true;

        [SerializeField]
        [Tooltip(Tooltips.forceDirection)]
        private Vector3 customForceDirection = Vector3.up;


        [SerializeField]
        private string[] tags = new string[] { Const.Tags.Player };

        #endregion

        public Vector3 GetForceDirection(ForceDirection forceDirection, Vector3 otherPosition)
        {
            switch (forceDirection)
            {
                case ForceDirection.collisionDirectionRelative:
                    {
                        var selfPosition = transform.position;
                        var vectorDelta = otherPosition - selfPosition;
                        var direction = vectorDelta.normalized;
                        return direction;
                    }

                case ForceDirection.objectLocalRight:
                    return transform.right;
                case ForceDirection.objectLocalLeft:
                    return -transform.right;

                case ForceDirection.objectLocalUp:
                    return transform.up;
                case ForceDirection.objectLocalDown:
                    return -transform.up;

                case ForceDirection.objectLocalForward:
                    return transform.forward;
                case ForceDirection.objectLocalBack:
                    return -transform.forward;

                case ForceDirection.customForceDirection:
                    {
                        var direction = customForceDirection.normalized;
                        return direction;
                    }

                default:
                    throw new System.NotImplementedException();
            }
        }
        public Vector3 Temp(ForceDirection forceDirection, Vector3 otherPosition)
        {
            var direction = GetForceDirection(forceDirection, otherPosition);
            if (ignoreVelocityY)
            {
                direction.y = 0f;
                direction.Normalize();
            }
            return direction;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var rigidbody = collision.rigidbody;
            ApplyKnockback(rigidbody);
        }

        private void OnTriggerEnter(Collider other)
        {
            var rigidbody = other.attachedRigidbody;
            ApplyKnockback(rigidbody);
        }
 
        private void ApplyKnockback(Rigidbody rigidbody)
        {
            var hasNotRigidbody = rigidbody == null;
            if (hasNotRigidbody)
            {
                return;
            }

            foreach (var tag in tags)
            {
                if (rigidbody.CompareTag(tag))
                {
                    var otherPosition = rigidbody.position;
                    var direction = Temp(forceDirection, otherPosition);
                    ForceHelpers.ApplyForce(rigidbody, direction, force, forceMode, resetDirectionVelocity);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            var origin = transform.position;
            var destination = origin + Temp(forceDirection, transform.position);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(origin, destination);
        }

    }