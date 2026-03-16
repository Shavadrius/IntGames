using IntGames.Domain.Abstractions;
using IntGames.Domain.Shared;
using IntGames.Domain.Users;

namespace IntGames.Domain.Players;

public sealed class Player(
    Guid id,
    FirstName firstName, 
    LastName lastName, 
    Patronymic? patronymic = null, 
    DateOnly? birthday = null) : Entity(id)
{
    public Guid? UserId { get; private set; }
    public FirstName FirstName { get; private set; } = firstName;
    public LastName LastName { get; private set; } = lastName;
    public Patronymic? Patronymic { get; private set; } = patronymic;
    public DateOnly? Birthday { get; private set; } = birthday;
    public VerifiedStatus VerifiedStatus { get; private set; }

    public int? Month => Birthday?.Month;
    public int? Year => Birthday?.Year;

    public Result<Player> BindUser(User user)
    {
        if (user.FirstName is null)
        {
            return PlayerErrors.FirstNameIsEmpty;
        }
        
        if (user.LastName is null)
        {
            return PlayerErrors.LastNameIsEmpty;
        }

        UserId = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Patronymic = user.Patronymic;

        return this;
    }

    public void Verify()
    {
        VerifiedStatus = VerifiedStatus.Verified;
    }
}
