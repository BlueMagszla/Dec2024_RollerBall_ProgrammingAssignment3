using System.Collections;
using UnityEngine;

public class ToggleableBlock : ToggleableBehaviour
{
    // MEMBERS
    [Header("Renderer")]
    [SerializeField] protected Material blockUpMat;
    [SerializeField] protected Material blockDownMat;
    [SerializeField] protected MeshRenderer meshRenderer;
    [Header("Animation")]
    [SerializeField] protected AnimationCurve animCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] protected float animDuration = .5f;
    [SerializeField] protected float animHeight = 1f;
    private Vector3 initalPosition;
    private Coroutine animRoutine;

    // METHODS
    public Vector3 CalcPosition(bool isToggled)
    {
        var offset = isToggled ? Vector3.up * animHeight : Vector3.zero;
        var position = initalPosition + offset;
        return position;
    }

    protected IEnumerator CoAnimBlock(bool isToggled)
    {
        var positionFrom = transform.position;
        var positionTo = CalcPosition(isToggled);
        var elapsedTime = 0f;

        do
        {
            elapsedTime += Time.deltaTime / animDuration;
            var animationTime = animCurve.Evaluate(elapsedTime);
            var interpolatedPosition = Vector3.Lerp(positionFrom, positionTo, animationTime);
            transform.position = interpolatedPosition;
            yield return null;
        }
        while (elapsedTime < 1f);
    }

    protected void AnimBlock(bool isToggled)
    {
        if (animRoutine != null)
        {
            StopCoroutine(animRoutine);
        }

        animRoutine = StartCoroutine(CoAnimBlock(isToggled));
    }

    public override void Toggle()
    {
        base.Toggle();
        AnimBlock(isToggled);
    }

    protected override void Awake()
    {
        base.Awake();
        initalPosition = transform.position;
        transform.position = CalcPosition(isToggled);
    }

    protected virtual void OnValidate()
    {
        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            if (invertToggle)
            {
                if (blockDownMat != null)
                {
                    meshRenderer.sharedMaterial = blockDownMat;
                }
            }
            else
            {
                if (blockUpMat != null)
                {
                    meshRenderer.sharedMaterial = blockUpMat;
                }
            }
        }
    }

}
