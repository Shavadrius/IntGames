using IntGames.Domain.Abstractions;

namespace IntGames.Domain.TournamentRequests;

public static class TournamentRequestErrors
{
    public static readonly IntGamesError ParticipantsAreEmpty = IntGamesError.Validation("Participants", "List of participants is empty.");
    public static IntGamesError MaxCapacity(int maxCapacity) => IntGamesError.Validation("Participants", $"Max participants count is {maxCapacity}");
}
