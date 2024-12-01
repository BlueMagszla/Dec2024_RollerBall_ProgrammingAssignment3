using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitBox : MonoBehaviour, IDamager<int>
{
    // PROPERTIES
    public abstract string[] Tags { get; }
    public abstract int Damage { get; }

    // EVENTS
    public event IDamageEvent<int> OnDamageCallback;

    // METHODS
    protected virtual void OnCollisionEnter(Collision collision)
    {
        foreach (var tag in Tags)
        {
            if (tag == collision.gameObject.tag)
            {
                var damageable = collision.gameObject.GetComponentInParent<IDamageable<int>>();
                if (damageable == null)
                    damageable = collision.gameObject.GetComponentInChildren<IDamageable<int>>();
                //var damageable = collision.gameObject.GetComponent<IDamageable<int>>();

                var isDamageable = damageable != null;

                if (isDamageable)
                {
                    var msg = new DamagerMsg<int>()
                    {
                        damage = Damage,
                        damager = this,
                        damagerObj = this.gameObject,
                        damageable = damageable,
                        damageableObj = collision.gameObject,
                    };

                    damageable.Damage(msg);
                    OnDamageCallback?.Invoke(msg);
                }
            }
        }
    }


}
