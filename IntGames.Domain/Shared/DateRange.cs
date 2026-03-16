using IntGames.Domain.Abstractions;

namespace IntGames.Domain.Shared;

public record DateRange()
{
    public static readonly IntGamesError Invalid = new("DateRange.Invalid", "Date Range is invalid.", ErrorType.Validation);
    public DateTimeOffset Start { get; init; }
    public DateTimeOffset? End { get; private set; }

    public int? DurationInDays => End is not null
        ? (int)((End - Start).Value.TotalDays)
        : null;

    public bool IsOngoing => End is null;

    public static Result<DateRange> Create(DateTimeOffset start, DateTimeOffset? end = null)
    {
        if (end is not null && start > end)
        {
            return Invalid;
        }

        return new DateRange
        {
            Start = start,
            End = end,
        };
    }

    public Result<DateRange> WithEndDate(DateTimeOffset end) => Create(Start, end);
}