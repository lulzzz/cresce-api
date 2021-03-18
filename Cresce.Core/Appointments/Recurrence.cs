using System;
using System.Linq;

namespace Cresce.Core.Appointments
{
    public record Recurrence
    {
        public string Type { get; init; } = string.Empty;
        public string[] WeekDays { get; init; } = new string[0];
        public DateTime Start { get; init; }
        public DateTime? End { get; init; }

        public virtual bool Equals(Recurrence? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type == other.Type &&
                   WeekDays.SequenceEqual(other.WeekDays) &&
                   Start.Equals(other.Start) &&
                   Nullable.Equals(End, other.End);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, WeekDays, Start, End);
        }
    }

    public static class RecurrenceType
    {
        public static string Weekly { get; } = nameof(Weekly).ToUpper();
    }

    public static class WeekDays
    {
        public static string Monday { get; } = nameof(Monday).ToUpper();
        public static string Tuesday { get; } = nameof(Tuesday).ToUpper();

        public static string[] FromNumbers(string recurrenceWeekDays)
        {
            return new[] {Monday, Tuesday};
        }
    }
}
