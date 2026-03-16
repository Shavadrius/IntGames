using IntGames.Domain.Abstractions;

namespace IntGames.Domain.Participants;

public static class ParticipantErrors
{
    public static IntGamesError InvalidFlowDirection(string message) => IntGamesError.InvalidFlowDirection("ParticipationStatus", message);
}
