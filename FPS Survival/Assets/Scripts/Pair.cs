public struct Pair<TFirst, TSecond>
{
    public TFirst First { get; set; }
    public TSecond Second { get; set; }

    public Pair(TFirst first, TSecond second)
    {
        First = first;
        Second = second;
    }

    public static bool operator ==(Pair<TFirst, TSecond> left, Pair<TFirst, TSecond> right)
    {
        return left.First.Equals(right.First) && left.Second.Equals(right.Second);
    }

    public static bool operator !=(Pair<TFirst, TSecond> left, Pair<TFirst, TSecond> right)
    {
        return !(left == right);
    }
}
