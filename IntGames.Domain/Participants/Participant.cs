using IntGames.Domain.Abstractions;
using IntGames.Domain.Players;
using IntGames.Domain.Shared;

namespace IntGames.Domain.Participants;

public sealed class Participant: Entity
{
    private Participant(
        Guid id, 
        Guid playerId, 
        FirstName firstName, 
        LastName lastName, 
        Patronymic? patronymic, 
        DateOnly? birthday) : base(id)
    {
        PlayerId = playerId;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        Birthday = birthday;
    }

    public Guid PlayerId { get; private init; }

    //Snapshot of Player info
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Patronymic? Patronymic { get; private set; }
    public DateOnly? Birthday { get; private set; }

    public Participant Update(
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

    public static Participant FromPlayer(Player player)
    {
        return new Participant(Guid.NewGuid(),
            playerId: player.Id,
            player.FirstName,
            player.LastName,
            player.Patronymic,
            player.Birthday);
    }
}
