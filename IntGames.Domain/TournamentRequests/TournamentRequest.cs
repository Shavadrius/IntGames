using IntGames.Domain.Abstractions;
using IntGames.Domain.Participants;

namespace IntGames.Domain.TournamentRequests;

public sealed class TournamentRequest : Entity
{
    private TournamentRequest(
        Guid id, 
        Guid tournamentId, 
        RequestType type) : base(id)
    {
        TournamentId = tournamentId;
        Type = type;
    }

    private readonly List<Participant> _participants = [];
    public IReadOnlyList<Participant> Participants => _participants.AsReadOnly();
    public Guid TournamentId { get; private init; }
    public RequestType Type { get; private init; }

    public static TournamentRequest Create(
            Guid tournamentId,
        RequestType type)
    {
        return new TournamentRequest(
            id: Guid.NewGuid(),
            tournamentId: tournamentId, 
            type);
    }
}
