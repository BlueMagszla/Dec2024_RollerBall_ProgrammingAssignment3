using System;

public delegate void IHealableEvent<TNumber>(TNumber health)
    where TNumber : struct,
          IComparable,
          IComparable<TNumber>,
          IConvertible,
          IEquatable<TNumber>,
          IFormattable;

public interface IHealable<TNumber>
    where TNumber : struct,
          IComparable,
          IComparable<TNumber>,
          IConvertible,
          IEquatable<TNumber>,
          IFormattable
{
    // PROPERTIES
    TNumber Health { get; }
    bool IsHealable { get; }

    // EVENTS
    event IHealableEvent<TNumber> OnHealableCallback;

    // METHODS
    void Heal(TNumber health);
}
