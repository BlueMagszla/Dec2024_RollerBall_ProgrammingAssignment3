using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ForceHelpers
{
    public static void ApplyForce(Rigidbody rigidbody, Vector3 direction, float force, ForceMode forceMode, bool resetDirectionVelocity)
    {
        var forceVector = force * direction;

        // Reset velocity
        if (resetDirectionVelocity)
        {
            var velocity = rigidbody.velocity;
            var planeNormal = direction.normalized;

            // In essence this squashes any velocity in provided 'planeNormal' direction.
            // If this were facing left or right, it gets rid of X velocity
            // If this were facing up or down, it gets rid of Y velocity
            // If this were facing forwards or back, it gets rid of Z velocity
            // ... and does the same for any arbitrary direction!
            var newVelocity = Vector3.ProjectOnPlane(velocity, planeNormal);

            // Apply new velocity
            rigidbody.velocity = newVelocity;
        }

        rigidbody.AddForce(forceVector, forceMode);
    }
}
