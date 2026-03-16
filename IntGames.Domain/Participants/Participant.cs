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

    public static Result<Participant> FromPlayer(Player player, Guid tournamentId)
    {
        return new Participant(Guid.NewGuid(),
            tournamentId,
            player.Id,
            player.FirstName,
            player.LastName,
            player.Patronymic,
            player.Birthday,
            ParticipationStatus.PendingApproval);
    }

}
