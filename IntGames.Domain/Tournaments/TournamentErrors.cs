using IntGames.Domain.Abstractions;

namespace IntGames.Domain.Tournaments;

public static class TournamentErrors
{
    public static readonly IntGamesError RequestNotFound = new("TournamentRequest.NotFound", "Request not found in tournament.", ErrorType.NotFound);
    public static readonly IntGamesError WrongTournamentInRequest = IntGamesError.Validation("Request.TournamentId", "Wrong tournament in request.");
    public static readonly IntGamesError RequestAlreadyProcessed = IntGamesError.Validation("Request", "Request already processed.");
    public static readonly IntGamesError RequestDuplication = IntGamesError.Validation("Request.Id", "Request duplication.");
    public static readonly IntGamesError PlayersAreNotUnique = IntGamesError.Validation("Participants", "Some players are already approved for this tournament.");
    public static readonly IntGamesError WrongTournamentType = IntGamesError.Validation("Participants", "Invalid participant count for type of this tournament.");
}
