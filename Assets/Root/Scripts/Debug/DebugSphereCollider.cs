using UnityEngine;

public class DebugSphereCollider : DebugCollider3D<SphereCollider>
{
    protected override void DrawColliderGizmo(ColliderMesh node)
    {
        var collider = node.collider as SphereCollider;

        var rotation = node.collider.transform.rotation;
        var scale = collider.transform.lossyScale;
        // Get position
        var center = rotation * collider.center;
        center.x *= scale.x;
        center.y *= scale.y;
        center.z *= scale.z;
        // Finalize scale
        var scaleLongest = scale.x > scale.y ? scale.x : scale.y;
        scaleLongest = scaleLongest > scale.z ? scaleLongest : scale.z;
        scale = Vector3.one * scaleLongest;
        // Sphere collider uses max axis as radius for whole collider
        var radius = collider.radius;
        scale *= radius * 2f; // diameter
        radius = scale.x / 2f; // use radius for Gizmo

        var position = node.collider.transform.position + center;

        Gizmos.DrawMesh(node.sharedMesh, position, rotation, scale);
        Gizmos.DrawWireSphere(position, radius);
    }

    protected override ColliderMesh GetMesh(SphereCollider collider)
    {
        return new ColliderMesh()
        {
            sharedMesh = GetPrimitiveSharedMesh(PrimitiveType.Sphere),
            collider = collider,
        };
    }

}
