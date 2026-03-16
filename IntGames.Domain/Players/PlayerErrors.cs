using IntGames.Domain.Abstractions;

namespace IntGames.Domain.Players;

public static class PlayerErrors
{
    public static readonly IntGamesError FirstNameIsEmpty = IntGamesError.Validation("FirstName", "User First Name is null or empty.");
    public static readonly IntGamesError LastNameIsEmpty = IntGamesError.Validation("LastName", "User Last Name is null or empty.");
}
