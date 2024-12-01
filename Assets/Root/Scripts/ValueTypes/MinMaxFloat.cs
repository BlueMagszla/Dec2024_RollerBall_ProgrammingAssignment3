[System.Serializable]
public struct MinMaxFloat
{
    public bool editorEditable;
    public float minLimit;
    public float maxLimit;
    public float min;
    public float max;

    private const float minLimitDefault = 0f;
    private const float maxLimitDefault = 1f;

    public MinMaxFloat(float min, float max)
    {
        this.editorEditable = true;
        this.min = min;
        this.max = max;
        this.minLimit = minLimitDefault;
        this.maxLimit = maxLimitDefault;
    }
    public MinMaxFloat(float min, float max, float absMin, float absMax, bool editorEditable = true)
    {
        this.editorEditable = editorEditable;
        this.min = min;
        this.max = max;
        this.minLimit = absMin;
        this.maxLimit = absMax;
    }

    public static implicit operator MinMaxFloat(MinMaxInt value)
    {
        return new MinMaxFloat(value.min, value.max, value.minLimit, value.maxLimit);
    }

    // ADDITION
    public static MinMaxFloat operator +(MinMaxFloat lhs, MinMaxFloat rhs)
    {
        lhs.min += rhs.min;
        lhs.max += rhs.max;
        return lhs;
    }
    // SUBTRACTION
    public static MinMaxFloat operator -(MinMaxFloat lhs, MinMaxFloat rhs)
    {
        lhs.min -= rhs.min;
        lhs.max -= rhs.max;
        return lhs;
    }
    // MULTIPLY
    public static MinMaxFloat operator *(MinMaxFloat lhs, MinMaxFloat rhs)
    {
        lhs.min *= rhs.min;
        lhs.max *= rhs.max;
        return lhs;
    }
    // DIVIDE
    public static MinMaxFloat operator /(MinMaxFloat lhs, MinMaxFloat rhs)
    {
        lhs.min /= rhs.min;
        lhs.max /= rhs.max;
        return lhs;
    }

    public static bool operator ==(MinMaxFloat lhs, MinMaxFloat rhs)
    {
        return (lhs.min == rhs.min & lhs.max == rhs.max);
    }
    public static bool operator !=(MinMaxFloat lhs, MinMaxFloat rhs)
    {
        return (lhs.min != rhs.min & lhs.max != rhs.max);
    }

    public override string ToString()
    {
        return $"MinMaxFloat({min}, {max} [{minLimit}/{maxLimit}]";
    }
    override public bool Equals(object o)
    {
        if (o == null)
            return false;

        return this == (MinMaxFloat)o;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}