using System;
using UnityEngine;

public struct DamagerMsg<TNumber>
    where TNumber : struct,
          IComparable,
          IComparable<TNumber>,
          IConvertible,
          IEquatable<TNumber>,
          IFormattable
{
    public TNumber              damage;
    public IDamageable<TNumber> damageable;
    public IDamager<TNumber>    damager;
    public GameObject           damageableObj;
    public GameObject           damagerObj;
}
