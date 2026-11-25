using System;

public class Clock : IEquatable<Clock>
{
    private const int MinutesPerDay = 24 * 60;
    private readonly int _totalMinutes;

    public Clock(int hours, int minutes)
    {
        int rawMinutes = hours * 60 + minutes;
        _totalMinutes = ((rawMinutes % MinutesPerDay) + MinutesPerDay) % MinutesPerDay;
    }

    public Clock Add(int minutesToAdd)
    {
        return new Clock(0, _totalMinutes + minutesToAdd);
    }

    public Clock Subtract(int minutesToSubtract)
    {
        return new Clock(0, _totalMinutes - minutesToSubtract);
    }

    public override string ToString()
    {
        int hours = _totalMinutes / 60;
        int minutes = _totalMinutes % 60;
        return $"{hours:D2}:{minutes:D2}";
    }

    public bool Equals(Clock other)
    {
        if (other is null)
        {
            return false;
        }
        return _totalMinutes == other._totalMinutes;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Clock);
    }

    public override int GetHashCode()
    {
        return _totalMinutes.GetHashCode();
    }
}