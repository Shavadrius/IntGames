using IntGames.Domain.Abstractions;
using IntGames.Domain.Shared;
using System.Reflection;

namespace IntGames.Domain.Tournaments;

public sealed class Tournament(
    Guid id,
    Title title, 
    DateRange dateRange) : Entity(id)
{
    public Title Title { get; private set; } = title;

    public DateRange DateRange { get; private set; } = dateRange;
}
