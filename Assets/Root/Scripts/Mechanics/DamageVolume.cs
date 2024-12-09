using UnityEngine;

public class DamageVolume : MonoBehaviour, IDamager<int>
{
    [SerializeField]
    private string[] tags = new string[] {
                Const.Tags.Untagged,
                Const.Tags.Player,
            };

    [SerializeField]
    private int damage = 1;
    [SerializeField]
    protected bool damageOnStay = false;

    public event IDamageEvent<int> OnDamageCallback;

    public virtual bool TryDamage(GameObject target)
    {
        var targetIsNotSelf = target.transform.root != this.transform.root;
        var tagIsMatch = GameObjectHelper.MatchTag(target, tags);
        var canDamage = tagIsMatch;
        if (canDamage)
        {
            // DEAL DAMAGE
            var damageable = target.GetComponentInParent<IDamageable<int>>();
            var hasHealthScript = damageable != null;
            if (hasHealthScript)
            {
                var damageMessage = new DamagerMsg<int>()
                {
                    damage = this.damage,
                    damager = this,
                    damagerObj = this.gameObject,
                    damageable = damageable,
                    damageableObj = target,
                };

                damageable.Damage(damageMessage);
                OnDamageCallback?.Invoke(damageMessage);
                return true;
            }
        }
        return false;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        var gameObject = collision.gameObject;
        TryDamage(gameObject);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    protected virtual void OnCollisionStay(Collision collision)
    {
        if (damageOnStay)
        {
            OnCollisionEnter(collision);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        var gameObject = other.gameObject;
        TryDamage(gameObject);
       
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (damageOnStay)
        {
            OnTriggerEnter(other);
        }
    }
}
