using UnityEngine;

public class DebugMeshCollider : DebugCollider3D<MeshCollider>
{
    [SerializeField] protected bool drawWireMesh;

    protected override void DrawColliderGizmo(ColliderMesh node)
    {
        var collider = node.collider as MeshCollider;

        var position = node.collider.transform.position;
        var rotation = node.collider.transform.rotation;
        var scale = collider.transform.lossyScale;

        Gizmos.DrawMesh(node.sharedMesh, position, rotation, scale);

        if (drawWireMesh)
        {
            Gizmos.DrawWireMesh(node.sharedMesh, position, rotation, scale);
        }
    }

    protected override ColliderMesh GetMesh(MeshCollider collider)
    {
        return new ColliderMesh()
        {
            sharedMesh = collider.GetComponent<MeshCollider>().sharedMesh,
            collider = collider,
        };
    }
}
