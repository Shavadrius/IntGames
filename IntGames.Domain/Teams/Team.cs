using IntGames.Domain.Abstractions;
using IntGames.Domain.Players;

namespace IntGames.Domain.Teams;

public sealed class Team(
    Guid id,
    Name name, 
    IReadOnlyList<Player> players) : Entity(id)
{
    public Name Name { get; private set; } = name;
    public IReadOnlyList<Player> Players { get; private set; } = players;
}
