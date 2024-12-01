using System;
using UnityEngine;

public struct DamageableMsg<TNumber>
    where TNumber : struct,
          IComparable,
          IComparable<TNumber>,
          IConvertible,
          IEquatable<TNumber>,
          IFormattable
{
    public DamagerMsg<TNumber> damagerMsg;
    public TNumber healthMax;
    public TNumber healthPreDamage;
    public TNumber healthPostDamage;

    // Properties to bypass message accessor
    public TNumber              Damage          => damagerMsg.damage;
    public IDamageable<TNumber> Damageable      => damagerMsg.damageable;
    public IDamager<TNumber>    Damager         => damagerMsg.damager;
    public GameObject           DamageableObj   => damagerMsg.damageableObj;
    public GameObject           DamagerObj      => damagerMsg.damagerObj;
}
