using System;

public delegate void IHealEvent<TNumber>(TNumber health)
    where TNumber : struct,
          IComparable,
          IComparable<TNumber>,
          IConvertible,
          IEquatable<TNumber>,
          IFormattable;

public interface IHealer<TNumber>
    where TNumber : struct,
          IComparable,
          IComparable<TNumber>,
          IConvertible,
          IEquatable<TNumber>,
          IFormattable
{
    // EVENTS
    event IHealEvent<TNumber> OnHealCallback;
}
