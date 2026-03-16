using IntGames.Domain.Abstractions;
using IntGames.Domain.Participants;

namespace IntGames.Domain.TournamentRequests;

public sealed class TournamentRequest : Entity
{
    public const int MaxCapacity = 24;
    private TournamentRequest(
        Guid id,
        Guid tournamentId,
        RequestType type,
        Name name) : base(id)
    {
        TournamentId = tournamentId;
        Type = type;
        Status = Status.PendingApproval;
        Name = name;
    }

    private readonly List<Participant> _participants = [];
    public IReadOnlyList<Participant> Participants => _participants.AsReadOnly();
    public Guid TournamentId { get; private init; }
    public Name Name { get; private set; }
    public RequestType Type { get; private init; }
    public Status Status { get; private set; }
    public bool IsPaid { get; private set; }

    public bool IsRegistered => Status is Status.Approved || Status is Status.AwaitingPayment;

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

    public Result AddParticipant(Participant participant)
    {
        if (_participants.Count >= MaxCapacity)
        {
            return Result.Failure(TournamentRequestErrors.MaxCapacity(MaxCapacity));
        }

        var existingPlayer = _participants.FirstOrDefault(p => p.PlayerId == participant.PlayerId);

        if (existingPlayer is null)
        {
            _participants.Add(participant);
        }

        return Result.Success();
    }

    public Result RemoveParticipant(Guid participantId)
    {
        var existingPlayer = _participants.FirstOrDefault(p => p.Id == participantId);
        if (existingPlayer is null)
        {
            return Result.Failure(TournamentRequestErrors.ParticipantNotFound);
        }
        _participants.Remove(existingPlayer);

        return Result.Success();
    }

    public Result Approve(bool isPaymentRequired = false)
    {
        Status = !isPaymentRequired || IsPaid ? Status.Approved : Status.AwaitingPayment;
        return Result.Success();
    }

    public Result Reject()
    {
        Status = Status.Rejected;
        return Result.Success();
    }

    public Result ConfirmPayment()
    {
        if (Status != Status.AwaitingPayment)
        {
            return Result.Failure(TournamentRequestErrors.InvalidFlowDirection("Participant invalid status."));
        }

        Status = Status.Approved;
        IsPaid = true;

        return Result.Success();
    }
}
