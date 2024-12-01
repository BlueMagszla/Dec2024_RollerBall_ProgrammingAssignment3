using System;

// TNumber is constrained as closely as possible to ints / floats
public delegate void IDamageableEvent<TNumber>(DamageableMsg<TNumber> damage)
    where TNumber : struct,
          IComparable,
          IComparable<TNumber>,
          IConvertible,
          IEquatable<TNumber>,
          IFormattable;

// TNumber is constrained as closely as possible to ints / floats
public interface IDamageable<TNumber>
    where TNumber : struct,
          IComparable,
          IComparable<TNumber>,
          IConvertible,
          IEquatable<TNumber>,
          IFormattable
{
    // PROPERTIES
    TNumber Health { get; }

    // EVENTS
    event IDamageableEvent<TNumber> OnDamageableCallback;

    // METHODS
    void Damage(DamagerMsg<TNumber> damagerMsg);
}
