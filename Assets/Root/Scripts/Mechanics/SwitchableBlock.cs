using UnityEngine;

public class SwitchableBlock : SwitchableBehaviour
{
    // MEMBERS
    [SerializeField] protected MeshRenderer meshRenderer;
    [SerializeField] protected Material onMaterial;
    [SerializeField] protected Material offMaterial;
    [SerializeField] new protected Collider collider;

    // METHODS
    protected virtual void Awake()
    {
        Switch(isSwitched);
    }

    public override void Switch(bool isSwitched)
    {
        base.Switch(isSwitched);

        collider.enabled = isSwitched;
        meshRenderer.sharedMaterial = isSwitched
            ? onMaterial
            : offMaterial;
    }

    protected virtual void OnValidate()
    {
        // MESH RENDERER
        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            if (isSwitched)
            {
                if (onMaterial != null)
                {
                    meshRenderer.sharedMaterial = onMaterial;
                }
            }
            else
            {
                if (offMaterial != null)
                {
                    meshRenderer.sharedMaterial = offMaterial;
                }
            }
        }

        // COLLIDER
        if (collider == null)
            collider = GetComponent<Collider>();

        if (collider != null)
        {
            collider.enabled = isSwitched;
        }
    }

}
