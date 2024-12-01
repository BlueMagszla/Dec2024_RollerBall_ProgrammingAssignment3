using System.Collections;
using UnityEngine;

public class PlayerHurtBox : HurtBox, IHealable<int>
{
    // MEMBERS
    [SerializeField] protected PlayerEntity playerEntity;
    [SerializeField] new protected MeshRenderer renderer;
    [SerializeField] protected Material damagedMaterial;
    private int health;

    // EVENTS
    public event IHealableEvent<int> OnHealableCallback;

    // PROPERTIES
    public override int Health
    {
        get { return health; }
        protected set { health = value; }
    }
    public override int HealthMax => playerEntity.Params.MaxHealth;
    public bool IsHealable { get; private set; } = true;

    // METHODS
    protected override void OnDamaged(DamageableMsg<int> msg)
    {
        // We are not damaged, kill routine
        if (msg.Damage < 1)
            return;

        // Get direction between enemy and player
        var damagerPosition = msg.DamagerObj.transform.position;
        var direction = transform.position - damagerPosition;
        direction.Normalize();

        // Create a vector and add force
        var force = direction * playerEntity.Params.DamageKnockback;
        playerEntity.Rigidbody.AddForce(force, ForceMode.Impulse);

        // Flash player to indicate damaged
        var duration = playerEntity.Params.DamageFlashDuration;
        StartCoroutine(CoDamageFlash(duration));
    }

    protected override void OnKilled(DamageableMsg<int> msg)
    {
        // Trigger death state
        playerEntity.PlayerFSM.ChangeState(PlayerFsmEvents.Died);
        renderer.sharedMaterial = damagedMaterial;
        IsHealable = false;
    }

    protected IEnumerator CoDamageFlash(float duration)
    {
        var material = renderer.sharedMaterial;
        renderer.sharedMaterial = damagedMaterial;
        yield return new WaitForSeconds(duration);
        renderer.sharedMaterial = material;
    }

    private void OnValidate()
    {
        if (playerEntity == null)
            playerEntity = GetComponentInParent<PlayerEntity>();

        if (renderer == null)
            renderer = GetComponentInParent<MeshRenderer>();
    }

    // IHealable<int>
    public void Heal(int recover)
    {
        var oldHealth = health;
        health = Mathf.Clamp(health + recover, 0, HealthMax);

        // would do more, out of time
        OnHealableCallback?.Invoke(health);
    }
}
