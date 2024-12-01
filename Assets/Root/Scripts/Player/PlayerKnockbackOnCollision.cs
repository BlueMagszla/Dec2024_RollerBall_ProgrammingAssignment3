using UnityEngine;

public class PlayerKnockbackOnCollision : MonoBehaviour
{
    // MEMBERS
    [SerializeField] protected HitBox hitBox;
    [SerializeField] protected PlayerEntity entity;

    // METHODS
    private void OnEnable()
    {
        hitBox.OnDamageCallback += HandleOnPlayerCollision;
    }

    private void OnDisable()
    {
        hitBox.OnDamageCallback -= HandleOnPlayerCollision;
    }

    void HandleOnPlayerCollision(DamagerMsg<int> msg)
    {
        if (msg.damageableObj.tag == Const.Tags.Enemy)
        {
            var force = Vector3.up * entity.Params.AttackKnockback;
            entity.Rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }

    private void OnValidate()
    {
        if (entity == null)
            entity = GetComponentInParent<PlayerEntity>();

        if (hitBox == null)
            hitBox = GetComponent<HitBox>();
    }
}
