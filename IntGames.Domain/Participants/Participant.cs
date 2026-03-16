using IntGames.Domain.Abstractions;
using IntGames.Domain.Players;
using IntGames.Domain.Shared;

namespace IntGames.Domain.Participants;

public sealed class Participant: Entity
{
    private Participant(
        Guid id, 
        Guid playerId, 
        Guid tournamentId, 
        FirstName firstName, 
        LastName lastName, 
        Patronymic? patronymic, 
        DateOnly? birthday, 
        ParticipationStatus status) : base(id)
    {
        PlayerId = playerId;
        TournamentId = tournamentId;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        Birthday = birthday;
        Status = status;
        IsPaid = false;
    }

    public Guid PlayerId { get; private init; }
    public Guid TournamentId { get; private init; }

    //Snapshot of Player info
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Patronymic? Patronymic { get; private set; }
    public DateOnly? Birthday { get; private set; }

    //Specific fields
    public ParticipationStatus Status { get; private set; }
    public bool IsPaid { get; private set; }

    public Result<Participant> Update(
        FirstName firstName,
        LastName lastName,
        Patronymic? patronymic = null,
        DateOnly? birthday = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        Birthday = birthday;

        return this;
    }

    public static Result<Participant> FromPlayer(Player player, Guid tournamentId)
    {
        return new Participant(Guid.NewGuid(),
            playerId: player.Id,
            tournamentId: tournamentId,
            player.FirstName,
            player.LastName,
            player.Patronymic,
            player.Birthday,
            ParticipationStatus.PendingApproval);
    }

    public Result<Participant> Approve(bool isPaymentRequired = false)
    {
        if (!isPaymentRequired || IsPaid)
        {
            Status = ParticipationStatus.Approved;
            return this;
        }

        return ParticipantErrors.InvalidFlowDirection("Cannot approve untill unpaid.");
    }

    public Result<Participant> Reject()
    {
        Status = ParticipationStatus.Rejected;
        return this;
    }

    public Result<Participant> ConfirmPayment()
    {
        if (Status != ParticipationStatus.PendingApproval)
        {
            return ParticipantErrors.InvalidFlowDirection("Participant invalid status.");
        }

        Status = ParticipationStatus.Approved;
        IsPaid = true;
        return this;
    }
}
