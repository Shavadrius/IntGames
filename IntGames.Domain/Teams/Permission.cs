namespace IntGames.Domain.Teams;

public sealed class Permission
{
    public static readonly Permission TeamAddNewPlayer = new(1, "teams:add_new_player");
    private Permission(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }
    public string Name { get; init; }
}
