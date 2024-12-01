using System.Collections.Generic;
using UnityEngine;

public abstract class DebugCollider3D<TCollider> : MonoBehaviour
    where TCollider : Collider
{
    // MEMBERS
    [SerializeField] protected Color gizmosColor = new Color32(155, 247, 74, 127); // Collider Green
    [SerializeField] protected ColliderMesh[] colliderMesh;

    // ABSTRACT METHODS
    protected abstract ColliderMesh GetMesh(TCollider collider);
    protected abstract void DrawColliderGizmo(ColliderMesh node);

    // METHODS
    protected ColliderMesh[] GetColliderMeshes(TCollider[] colliders)
    {
        List<ColliderMesh> collection = new List<ColliderMesh>();
        foreach (var collider in colliders)
        {
            var data = GetMesh(collider);

            // Only add if mesh exists
            if (data.sharedMesh != null && data.collider != null)
            {
                collection.Add(data);
            }
        }

        return collection.ToArray();
    }

    protected Mesh GetPrimitiveSharedMesh(PrimitiveType primitiveType)
    {
        var gobj = GameObject.CreatePrimitive(primitiveType);
        var mesh = gobj.GetComponent<MeshFilter>().sharedMesh;
        DestroyImmediate(gobj);
        return mesh;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;

        foreach (var node in colliderMesh)
        {
            if (node.collider != null && node.collider.enabled)
            {
                DrawColliderGizmo(node);
            }
        }
    }

    protected void Reset()
    {
        if (colliderMesh == null || colliderMesh.Length == 0)
        {
            var colliders = GetComponentsInChildren<TCollider>();
            colliderMesh = GetColliderMeshes(colliders);
        }
    }

}
