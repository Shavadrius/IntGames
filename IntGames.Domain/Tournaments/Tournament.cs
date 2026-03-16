using IntGames.Domain.Abstractions;
using IntGames.Domain.Participants;
using IntGames.Domain.Shared;

namespace IntGames.Domain.Tournaments;

public sealed class Tournament : Entity
{
    private Tournament(
        Guid id,
        Title title,
        TournamentType type,
        DateRange dateRange) : base(id)
    {
        Title = title;
        Type = type;
        DateRange = dateRange;
    }

    private readonly List<Participant> _participants = [];
    public IReadOnlyList<Participant> Participants => _participants.AsReadOnly();

    public TournamentType Type { get; }
    public Title Title { get; private set; }
    public DateRange DateRange { get; private set; }

    public static Result<Tournament> Create(
        Title title,
        TournamentType type,
        DateRange dateRange)
    {
        return new Tournament(
            id: Guid.NewGuid(),
            title,
            type,
            dateRange);
    }
}
