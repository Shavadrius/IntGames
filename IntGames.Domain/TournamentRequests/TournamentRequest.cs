using IntGames.Domain.Abstractions;
using IntGames.Domain.Participants;
using IntGames.Domain.Players;

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
        Status = ParticipationStatus.PendingApproval;
        Name = name;
    }

    private readonly List<Participant> participants = [];
    public IReadOnlyList<Participant> Participants => participants.AsReadOnly();
    public Guid TournamentId { get; private init; }
    public Name Name { get; private set; }
    public RequestType Type { get; private init; }
    public ParticipationStatus Status { get; private set; }
    public bool IsPaid { get; private set; }

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

    public Result AddParticipant(Participant participant)
    {
        if (participants.Count >= MaxCapacity)
        {
            return Result.Failure(TournamentRequestErrors.MaxCapacity(MaxCapacity));
        }

        var existingPlayer = participants.FirstOrDefault(p => p.PlayerId == participant.PlayerId);

        if (existingPlayer is null)
        {
            participants.Add(participant);
        }

        return Result.Success();
    }

    public Result RemoveParticipant(Guid participantId)
    {
        var existingPlayer = participants.FirstOrDefault(p => p.Id == participantId);
        if (existingPlayer is not null)
        {
            participants.Remove(existingPlayer);
        }
        return Result.Success();
    }

    public Result Approve(bool isPaymentRequired = false)
    {
        Status = !isPaymentRequired || IsPaid ? ParticipationStatus.Approved : ParticipationStatus.AwaitingPayment;
        return Result.Success();
    }

    public Result Reject()
    {
        Status = ParticipationStatus.Rejected;
        return Result.Success();
    }

    public Result ConfirmPayment()
    {
        if (Status != ParticipationStatus.AwaitingPayment)
        {
            return Result.Failure(TournamentRequestErrors.InvalidFlowDirection("Participant invalid status."));
        }

        Status = ParticipationStatus.Approved;
        IsPaid = true;

        return Result.Success();
    }
}
