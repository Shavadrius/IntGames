using IntGames.Domain.Abstractions;
using IntGames.Domain.Shared;
using IntGames.Domain.Users.Events;

namespace IntGames.Domain.Users;

public sealed class User(
    Guid id,
    Email email, 
    FirstName? firstName = null, 
    LastName? lastName = null, 
    Patronymic? patronymic = null): Entity(id)
{
    public Email Email{ get; private set; } = email;
    public FirstName? FirstName { get; private set; } = firstName;
    public LastName? LastName { get; private set; } = lastName;
    public Patronymic? Patronymic { get; private set; } = patronymic;
    public string IdentityId { get; private set; } = string.Empty;

    public static Result<User> Create(
        Email email,
        FirstName? firstName = null,
        LastName? lastName = null,
        Patronymic? patronymic = null)
    {
        var user = new User(Guid.NewGuid(), email, firstName, lastName, patronymic);

        user.RegisterDomainEvent(new UserCreateDomainEvent(user.Id));

        return user;
    }

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}
