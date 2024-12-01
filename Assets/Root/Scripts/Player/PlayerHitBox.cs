using UnityEngine;

public class PlayerHitBox : HitBox
{
    [SerializeField] protected PlayerEntity playerEntity;
    private readonly string[] tags = { Const.Tags.Enemy };

    public override string[] Tags   => tags;
    public override int Damage      => playerEntity.Params.AttackPower;

    protected virtual void OnValidate()
    {
        if (playerEntity == null)
            playerEntity = GetComponentInParent<PlayerEntity>();
    }
}
