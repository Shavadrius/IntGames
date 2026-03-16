namespace IntGames.Domain.Shared;

public record DateRange()
{
    public DateTimeOffset Start { get; init; }
    public DateTimeOffset? End { get; private set; }

    public int? DurationInDays => End is not null
        ? (int)((End - Start).Value.TotalDays)
        : null;

    public bool IsOngoing => End is null;

    public static DateRange Create(DateTimeOffset start, DateTimeOffset? end = null)
    {
        if (end is not null && start > end)
        {
            throw new ArgumentException("End date cannot be more than start date.");
        }

        return new DateRange
        {
            Start = start,
            End = end,
        };
    }

    public void SetEndDate(DateTimeOffset end)
    {
        if (Start > end)
        {
            throw new ArgumentException("End date cannot be more than start date.");
        }
        End = end;
    }
}