using IntGames.Domain.Abstractions;
using IntGames.Domain.Participants;
using IntGames.Domain.Players;

namespace IntGames.Domain.TournamentRequests;

public sealed class TournamentRequest : Entity
{
    public readonly int MaxCapacity = 24;
    private TournamentRequest(
        Guid id,
        Guid tournamentId,
        RequestType type,
        Name name) : base(id)
    {
        TournamentId = tournamentId;
        Type = type;
        Status = ParticipationStatus.PendingApproval;
        Name = name;
    }

    private readonly List<Player> _players = [];
    public IReadOnlyList<Player> Players => _players.AsReadOnly();
    public Guid TournamentId { get; private init; }
    public Name Name { get; private set; }
    public RequestType Type { get; private init; }
    public ParticipationStatus Status { get; private set; }

    public bool IsRegistered => Status is ParticipationStatus.Approved || Status is ParticipationStatus.AwaitingPayment;

    public static TournamentRequest Create(
        Guid tournamentId,
        RequestType type,
        Name name)
    {
        return new TournamentRequest(
            id: Guid.NewGuid(),
            tournamentId: tournamentId, 
            type,
            name);
    }

    public Result AddParticipant(Player player)
    {
        if (_players.Count >= MaxCapacity)
        {
            return Result.Failure(TournamentRequestErrors.MaxCapacity(MaxCapacity));
        }
        var existingPlayer = _players.FirstOrDefault(p => p.Id == player.Id);
        if (existingPlayer is not null)
        {
            _players.Add(player);
        }

        return Result.Success();
    }

    public Result RemoveParticipant(Guid playerId)
    {
        var existingPlayer = _players.FirstOrDefault(p => p.Id == playerId);
        if (existingPlayer is not null)
        {
            _players.Remove(existingPlayer);
        }
        return Result.Success();
    }
}
