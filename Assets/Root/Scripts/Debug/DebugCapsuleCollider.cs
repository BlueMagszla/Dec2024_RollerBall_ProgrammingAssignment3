using UnityEngine;

public class DebugCapsuleCollider : DebugCollider3D<CapsuleCollider>
{
    private static Quaternion GetCapsuleColliderOrientation(CapsuleCollider collider)
    {
        var direction = collider.direction;

        switch (direction)
        {
            case 0: return Quaternion.Euler( 0,  0, 90); // X
            case 1: return Quaternion.Euler( 0,  0,  0); // Y
            case 2: return Quaternion.Euler(90,  0,  0); // Z

            default:
                throw new System.NotImplementedException();
        }
    }

    protected override void DrawColliderGizmo(ColliderMesh node)
    {
        var collider = node.collider as CapsuleCollider;

        var center = collider.center;
        var orientation = GetCapsuleColliderOrientation(collider);
        var radius = collider.radius * 2f;
        var height = collider.height / 2f; // div2 to compensate for mesh
        // TODO:
        // Make semi-sphere cap and cylinder
        var size = new Vector3(radius, height, radius);

        var position = node.collider.transform.position + center;
        var rotation = node.collider.transform.rotation * orientation;
        var scale = collider.transform.lossyScale;
        scale.x *= size.x;
        scale.y *= size.y;
        scale.z *= size.z;

        Gizmos.DrawMesh(node.sharedMesh, position, rotation, scale);
    }

    protected override ColliderMesh GetMesh(CapsuleCollider collider)
    {
        return new ColliderMesh()
        {
            sharedMesh = GetPrimitiveSharedMesh(PrimitiveType.Capsule),
            collider = collider,
        };
    }
}
