using UnityEngine;

public class DebugBoxCollider : DebugCollider3D<BoxCollider>
{
    protected override void DrawColliderGizmo(ColliderMesh node)
    {
        var collider = node.collider as BoxCollider;

        var rotation = node.collider.transform.rotation;
        var scale = collider.transform.lossyScale;

        var center = rotation * collider.center;
        center.x *= scale.x;
        center.y *= scale.y;
        center.z *= scale.z;

        var position = node.collider.transform.position + center;

        // Scale
        var size = collider.size;
        // Mesh Scale
        var meshScale = new Vector3(
            scale.x * size.x,
            scale.y * size.y,
            scale.z * size.z);

        Gizmos.DrawMesh(node.sharedMesh, position, rotation, meshScale);
    }

    protected override ColliderMesh GetMesh(BoxCollider collider)
    {
        return new ColliderMesh()
        {
            sharedMesh = GetPrimitiveSharedMesh(PrimitiveType.Cube),
            collider = collider,
        };
    }
}
