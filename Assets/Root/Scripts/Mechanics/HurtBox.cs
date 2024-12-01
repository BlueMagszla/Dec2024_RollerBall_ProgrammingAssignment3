using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HurtBox : MonoBehaviour, IDamageable<int>
{
    // PROPERTIES
    public abstract int Health { get; protected set; }
    public abstract int HealthMax { get; }

    // EVENTS
    public event IDamageableEvent<int> OnDamageableCallback;
    public event IDamageableEvent<int> OnDamageableZeroCallback;

    // METHODS
    public void Damage(DamagerMsg<int> damagerMsg)
    {
        var healthRemaining = Mathf.Clamp(Health - damagerMsg.damage, 0, HealthMax);
        Health = healthRemaining;

        var msg = new DamageableMsg<int>()
        {
            damagerMsg = damagerMsg, // pass it along

            healthMax = HealthMax,
            healthPreDamage = Health,
            healthPostDamage = healthRemaining,
        };

        var isHealthZero = Health <= 0;
        if (isHealthZero)
        {
            OnDamageableZeroCallback?.Invoke(msg);
            OnKilled(msg);
        }

        OnDamaged(msg);
        OnDamageableCallback?.Invoke(msg);
    }

    protected abstract void OnDamaged(DamageableMsg<int> msg);
    protected abstract void OnKilled(DamageableMsg<int> msg);

    protected virtual void Awake()
    {
        Health = HealthMax;
    }

    protected virtual void Update()
    {
#if UNITY_EDITOR
        // This checks to see if you killed the enemy in the inspector
        var msg = new DamagerMsg<int>()
        {
            damage = 0,
            damager = null,
            damagerObj = this.gameObject,
            damageable = this,
            damageableObj = this.gameObject,
        };
        Damage(msg);
#endif
    }
}
