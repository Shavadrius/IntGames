using IntGames.Domain.Abstractions;

namespace IntGames.Domain.Players;

public static class PlayerErrors
{
    public static readonly IntGamesError FirstNameIsEmpty = new(
        "Player.Invalid",
        "User First Name is null or empty.",
        ErrorType.Validation);

    public static readonly IntGamesError LastNameIsEmpty = new(
        "Player.Invalid",
        "User Last Name is null or empty.",
        ErrorType.Validation);
}
