[System.Serializable]
public struct MinMaxInt
{
    public bool editorEditable;
    public int minLimit;
    public int maxLimit;
    public int min;
    public int max;

    private const int minLimitDefault = 0;
    private const int maxLimitDefault = 10;

    public MinMaxInt(int min, int max, bool editableMinMax = true)
    {
        this.editorEditable = editableMinMax;
        this.min = min;
        this.max = max;
        this.minLimit = minLimitDefault;
        this.maxLimit = maxLimitDefault;
    }
    public MinMaxInt(int min, int max, int absMin, int absMax, bool editorEditable = true)
    {
        this.editorEditable = editorEditable;
        this.min = min;
        this.max = max;
        this.minLimit = absMin;
        this.maxLimit = absMax;
    }

    // ADDITION
    public static MinMaxInt operator +(MinMaxInt lhs, MinMaxInt rhs)
    {
        lhs.min += rhs.min;
        lhs.max += rhs.max;
        return lhs;
    }
    // SUBTRACTION
    public static MinMaxInt operator -(MinMaxInt lhs, MinMaxInt rhs)
    {
        lhs.min -= rhs.min;
        lhs.max -= rhs.max;
        return lhs;
    }
    // MULTIPLY
    public static MinMaxInt operator *(MinMaxInt lhs, MinMaxInt rhs)
    {
        lhs.min *= rhs.min;
        lhs.max *= rhs.max;
        return lhs;
    }
    // DIVIDE
    public static MinMaxInt operator /(MinMaxInt lhs, MinMaxInt rhs)
    {
        lhs.min /= rhs.min;
        lhs.max /= rhs.max;
        return lhs;
    }

    public static bool operator ==(MinMaxInt lhs, MinMaxInt rhs)
    {
        return (lhs.min == rhs.min & lhs.max == rhs.max);
    }
    public static bool operator !=(MinMaxInt lhs, MinMaxInt rhs)
    {
        return (lhs.min != rhs.min & lhs.max != rhs.max);
    }

    new public string ToString()
    {
        return $"MinMaxInt({min}, {max} [{minLimit}/{maxLimit}]";
    }
    override public bool Equals(object o)
    {
        if (o == null)
            return false;

        return this == (MinMaxInt)o;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}