using UnityEngine;

public class KillVolume : MonoBehaviour, IDamager<int>
{
    public event IDamageEvent<int> OnDamageCallback;

    private void OnTriggerEnter(Collider other)
    {
        // We don't care about tags, kill anything we can
        var damageable = other.GetComponentInParent<IDamageable<int>>();
        var isDamageable = damageable != null;

        if (isDamageable)
        {
            var msg = new DamagerMsg<int>()
            {
                damage = int.MaxValue,
                damager = this,
                damagerObj = this.gameObject,
                damageable = damageable,
                damageableObj = other.gameObject, 
            };

            damageable.Damage(msg);
            OnDamageCallback?.Invoke(msg);
        }
    }
}
