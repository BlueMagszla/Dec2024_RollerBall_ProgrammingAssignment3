using System;

public delegate void IDamageEvent<TNumber>(DamagerMsg<TNumber> damage)
    where TNumber : struct,
          IComparable,
          IComparable<TNumber>,
          IConvertible,
          IEquatable<TNumber>,
          IFormattable;

public interface IDamager<TNumber>
    where TNumber : struct,
          IComparable,
          IComparable<TNumber>,
          IConvertible,
          IEquatable<TNumber>,
          IFormattable
{
    // EVENTS
    event IDamageEvent<TNumber> OnDamageCallback;
}
